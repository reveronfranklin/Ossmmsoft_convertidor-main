// Controllers/GatewayController.cs

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class GatewayController : ControllerBase
{
    private readonly IHttpService _httpService;
    private readonly ILogger<GatewayController> _logger;

    public GatewayController(IHttpService httpService, ILogger<GatewayController> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

  [HttpPost("execute")]
    public async Task<ActionResult<GatewayResponse>> ExecuteRequest([FromBody] HttpRequestModel request)
    {
        if (string.IsNullOrEmpty(request.Url))
        {
            return BadRequest("URL is required");
        }

        if (!IsValidUrl(request.Url))
        {
            return BadRequest("Invalid URL format");
        }

        _logger.LogInformation("Processing gateway request to {Url}", request.Url);

        var result = await _httpService.ExecuteRequestAsync(request);

        if (result.Success)
        {
            return Ok(result);
        }
        else
        {
            return StatusCode(result.StatusCode, result);
        }
    }
    [HttpPost("batch")]
    public async Task<ActionResult<List<GatewayResponse>>> ExecuteBatchRequests([FromBody] List<HttpRequestModel> requests)
    {
        if (requests == null || !requests.Any())
        {
            return BadRequest("No requests provided");
        }

        if (requests.Count > 10) // Límite para evitar abuso
        {
            return BadRequest("Maximum 10 requests allowed per batch");
        }

        var tasks = requests.Select(request => _httpService.ExecuteRequestAsync(request));
        var results = await Task.WhenAll(tasks);

        return Ok(results.ToList());
    }


    [HttpPost("form-data")]
    [DisableRequestSizeLimit] // O usar [RequestSizeLimit(10485760)] para 10MB
    public async Task<ActionResult<GatewayResponse>> ExecuteFormDataRequest()
    {
        try
        {
            // Validar que sea multipart/form-data
            if (!Request.HasFormContentType)
            {
                return BadRequest("Content-Type must be multipart/form-data");
            }

            var form = await Request.ReadFormAsync();

            // Obtener URL desde form-data
            var url = form["url"].FirstOrDefault();
            if (string.IsNullOrEmpty(url))
            {
                return BadRequest("URL is required in form-data");
            }

            if (!IsValidUrl(url))
            {
                return BadRequest("Invalid URL format");
            }

            // Crear modelo de request
            var request = new HttpRequestModel
            {
                Url = url,
                Method = form["method"].FirstOrDefault() ?? "POST", // Default POST para form-data
                Headers = new Dictionary<string, string>(),
                TimeoutMs = int.TryParse(form["timeoutMs"].FirstOrDefault(), out var timeout) 
                    ? timeout : 30000
            };

            // Procesar headers
            var headerKeys = form.Keys.Where(k => k.StartsWith("header_"));
            foreach (var key in headerKeys)
            {
                var headerName = key.Substring(7); // Remover "header_" prefix
                request.Headers[headerName] = form[key].FirstOrDefault();
            }

            // Procesar archivos
            if (form.Files != null && form.Files.Count > 0)
            {
                request.Files = new List<FileModel>();
                
                foreach (var file in form.Files)
                {
                    if (file.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        await file.CopyToAsync(ms);
                        
                        request.Files.Add(new FileModel
                        {
                            FileName = file.FileName,
                            ContentType = file.ContentType,
                            Data = ms.ToArray(),
                            FormFieldName = file.Name
                        });
                    }
                }
            }

            // Procesar campos de formulario (excepto los ya procesados)
            var formFields = form
                .Where(f => !f.Key.StartsWith("header_") && 
                           f.Key != "url" && 
                           f.Key != "method" && 
                           f.Key != "timeoutMs")
                .ToDictionary(f => f.Key, f => f.Value.ToString());

            if (formFields.Any())
            {
                request.FormData = formFields;
            }

            _logger.LogInformation("Processing form-data request to {Url} with {FileCount} files", 
                url, request.Files?.Count ?? 0);

            var result = await _httpService.ExecuteRequestAsync(request);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(result.StatusCode, result);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing form-data request");
            return StatusCode(500, new GatewayResponse 
            { 
                Success = false, 
                ErrorMessage = "Internal server error processing form-data",
                StatusCode = 500
            });
        }
    }


    private bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) 
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}