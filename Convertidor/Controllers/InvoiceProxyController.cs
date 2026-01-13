using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Convertidor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceProxyController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<InvoiceProxyController> _logger;

        public InvoiceProxyController(IHttpClientFactory httpClientFactory, ILogger<InvoiceProxyController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadInvoices()
        {
            try
            {
                // Verificar que la solicitud tiene form-data
                if (!Request.HasFormContentType)
                {
                    return BadRequest("Expected form-data content type");
                }

                var form = await Request.ReadFormAsync();
                var files = form.Files;

                if (files == null || files.Count == 0)
                {
                    return BadRequest("No files uploaded");
                }

                // Crear cliente HTTP
                using var httpClient = _httpClientFactory.CreateClient();
                
                // Configurar headers
                httpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImlhdCI6MTUxNjIzOTAyMn0.KMUFsIDTnFmyG3nMiGM6H9FNFUROf3wh7SmqJp-QV30");

                // Crear multipart form data
                using var formData = new MultipartFormDataContent();

                // Agregar archivos al form data
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileContent = new StreamContent(file.OpenReadStream());
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                        formData.Add(fileContent, "file", file.FileName);
                        
                        _logger.LogInformation($"Added file: {file.FileName}, Size: {file.Length} bytes");
                    }
                }

                // Realizar la llamada al webhook
                var response = await httpClient.PostAsync(
                    "http://216.244.81.116:5678/webhook/invoices/load", 
                    formData);

                // Leer respuesta
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Webhook call successful: {response.StatusCode}");
                    return Ok(new { 
                        message = "Files processed successfully",
                        webhookResponse = responseContent 
                    });
                }
                else
                {
                    _logger.LogError($"Webhook call failed: {response.StatusCode} - {responseContent}");
                    return StatusCode((int)response.StatusCode, new { 
                        error = "Webhook call failed", 
                        details = responseContent 
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing invoice upload");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }
    }
}