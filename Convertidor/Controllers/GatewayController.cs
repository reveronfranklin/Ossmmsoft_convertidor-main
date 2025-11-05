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

    private bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) 
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}