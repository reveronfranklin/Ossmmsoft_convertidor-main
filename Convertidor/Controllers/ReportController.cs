using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;
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
public async Task<IActionResult> GetReport([FromBody] dynamic jsonBody) // Usa dynamic para máxima flexibilidad
{
    try
    {
        var settings = _configuration.GetSection("Settings").Get<Settings>();
        var destino = settings.ExcelFiles;

        // 1. Generar nombre de archivo aleatorio (sin depender de campos del JSON)
        string nombreArchivo = $"{Guid.NewGuid()}.pdf";
        string filePath = Path.Combine(destino, nombreArchivo);

        // 2. Token de autenticación (x-refresh-token)
        string refreshToken = "eH6FFBS6Z8jBSMLTvcVHcc/SnGrA4y3pbrR5c76dN1UD+qK91AUv8hH7HJPHni1NEwGkC8IqGNpPDiVAm1ZCYw==";
       
        string usuario = jsonBody.GetProperty("Usuario").GetString() ?? "OSSMMADEV";
        var sisUsuario = await _sisUsuarioRepository.GetByLogin(usuario);
        if (sisUsuario != null) refreshToken = sisUsuario.REFRESHTOKEN;

        // 3. URL de la API (usa la proporcionada o una por defecto)
    
        string apiUrl = jsonBody.GetProperty("Report").GetString() ?? "http://ossmmasoft.com.ve:4000/api-v1.0/payment-orders/pdf/report";
        // 4. Pasar el body COMPLETO tal cual a la API externa
        string jsonRequest = JsonConvert.SerializeObject(jsonBody);

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("x-refresh-token", refreshToken);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                byte[] pdfBytes = await response.Content.ReadAsByteArrayAsync();
                await System.IO.File.WriteAllBytesAsync(filePath, pdfBytes);
                return PhysicalFile(filePath, "application/pdf", nombreArchivo);
            }
            else
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
        }
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Error interno: {ex.Message}");
    }
}
    
}