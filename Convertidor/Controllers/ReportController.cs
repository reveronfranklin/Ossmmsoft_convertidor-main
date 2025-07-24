using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json.Linq;

namespace Convertidor.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ReportController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ISisUsuarioRepository _sisUsuarioRepository;

    public ReportController(IConfiguration configuration, ISisUsuarioRepository sisUsuarioRepository)
    {
        _configuration = configuration;
        _sisUsuarioRepository = sisUsuarioRepository;
    }


    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> GetReport([FromBody] JObject jsonBody)
    {
        var result = "";
        var settings = _configuration.GetSection("Settings").Get<Settings>();
        var destino = settings.ExcelFiles;

        try
        {
            // Generar un nombre de archivo aleatorio con Guid
            var nombreAleatorio = Guid.NewGuid() + ".pdf";
            var filePatch =
                $"{destino}/{nombreAleatorio}"; // Ejemplo: "/ruta/archivos/a1b2c3d4-1234-5678-91011.pdf"

            // Token de autenticación (x-refresh-token)
            var refreshToken =
                "eH6FFBS6Z8jBSMLTvcVHcc/SnGrA4y3pbrR5c76dN1UD+qK91AUv8hH7HJPHni1NEwGkC8IqGNpPDiVAm1ZCYw==";
            var usuario = jsonBody["Usuario"]?.ToString() ?? "OSSMMADEV";
            var sisUsuario = await _sisUsuarioRepository.GetByLogin(usuario);
            if (sisUsuario != null) refreshToken = sisUsuario.REFRESHTOKEN;

            // URL de la API
            var apiUrl = jsonBody["Report"]?.ToString() ??
                         "http://ossmmasoft.com.ve:4000/api-v1.0/payment-orders/pdf/report";

            // Pasar el body recibido directamente
            var jsonRequest = jsonBody.ToString();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-refresh-token", refreshToken);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var pdfBytes = await response.Content.ReadAsByteArrayAsync();

                    try
                    {
                        if (System.IO.File.Exists(filePatch)) System.IO.File.Delete(filePatch);

                        System.IO.File.WriteAllBytes(filePatch, pdfBytes);
                        Console.WriteLine("PDF guardado en: " + filePatch);
                    }
                    catch (Exception ex)
                    {
                        result = "Error al guardar el PDF: " + ex.Message;
                    }

                    // Devolver el archivo PDF al cliente
                    var provider = new FileExtensionContentTypeProvider();
                    if (!provider.TryGetContentType(filePatch, out var contentType)) contentType = "application/pdf";

                    return File(await System.IO.File.ReadAllBytesAsync(filePatch), contentType, nombreAleatorio);
                }

                // Manejar error (opcional: guardar un PDF vacío o de error)
                var bytes = await System.IO.File.ReadAllBytesAsync($"{destino}/NO_DATA.pdf");
                return File(bytes, "application/pdf", "NO_DATA.pdf");
            }
        }
        catch (HttpRequestException ex)
        {
            result = $"Error en la solicitud HTTP: {ex.Message}";
        }
        catch (Exception ex)
        {
            result = $"Error inesperado: {ex.Message}";
        }

        return Ok(result);
    }
}