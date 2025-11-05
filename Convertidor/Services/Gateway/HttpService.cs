// Services/HttpService.cs
using System.Text;
using System.Text.Json;

public class HttpService : IHttpService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<HttpService> _logger;

    public HttpService(IHttpClientFactory httpClientFactory, ILogger<HttpService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<GatewayResponse> ExecuteRequestAsync(HttpRequestModel request)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        
        try
        {
            using var client = _httpClientFactory.CreateClient("GatewayClient");
            client.Timeout = TimeSpan.FromSeconds(request.TimeoutSeconds);

            var httpRequest = new HttpRequestMessage(new HttpMethod(request.Method), request.Url);

            // Agregar headers
            foreach (var header in request.Headers)
            {
                if (!httpRequest.Headers.TryAddWithoutValidation(header.Key, header.Value))
                {
                    httpRequest.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            // Agregar body si es necesario
            if (request.Body != null && (request.Method == "POST" || request.Method == "PUT" || request.Method == "PATCH"))
            {
                var jsonContent = JsonSerializer.Serialize(request.Body);
                httpRequest.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }

            var response = await client.SendAsync(httpRequest);
            var contentString = await response.Content.ReadAsStringAsync();

            stopwatch.Stop();

            _logger.LogInformation("Request to {Url} completed with status {StatusCode} in {ElapsedMs}ms", 
                request.Url, (int)response.StatusCode, stopwatch.ElapsedMilliseconds);

            // Intentar parsear el contenido como JSON, si falla mantener como string
            object contentObject = contentString;
            if (!string.IsNullOrEmpty(contentString))
            {
                try
                {
                    // Verificar si el content-type indica que es JSON
                    var contentType = response.Content.Headers.ContentType?.MediaType;
                    if (contentType?.Contains("json") == true || IsValidJson(contentString))
                    {
                        contentObject = JsonSerializer.Deserialize<JsonElement>(contentString);
                    }
                }
                catch (JsonException)
                {
                    // Si no es JSON válido, mantener como string
                    contentObject = contentString;
                }
            }

            return new GatewayResponse
            {
                Success = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode,
                Content = contentObject,
                Headers = response.Headers.ToDictionary(h => h.Key, h => string.Join(", ", h.Value)),
                ResponseTime = stopwatch.Elapsed
            };
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex, "Error executing request to {Url}", request.Url);
            
            return new GatewayResponse
            {
                Success = false,
                StatusCode = 500,
                ErrorMessage = ex.Message,
                ResponseTime = stopwatch.Elapsed
            };
        }
    }

    private bool IsValidJson(string strInput)
    {
        if (string.IsNullOrWhiteSpace(strInput)) return false;
        
        strInput = strInput.Trim();
        if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || 
            (strInput.StartsWith("[") && strInput.EndsWith("]")))
        {
            try
            {
                JsonSerializer.Deserialize<JsonElement>(strInput);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }
        return false;
    }
}