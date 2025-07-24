using System.Text;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.Pagos;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Sis;
using Convertidor.Services.Adm;
using Convertidor.Services.Sis;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdmLotePagoController : ControllerBase
    {
       
        private readonly IAdmLotePagoService _service;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IConfiguration _configuration;

        public AdmLotePagoController(IAdmLotePagoService service,ISisUsuarioRepository sisUsuarioRepository,
            IConfiguration configuration)
        {
            _service = service;
            _sisUsuarioRepository = sisUsuarioRepository;
            _configuration = configuration;
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ReconstruirSearchText(AdmLotePagoFilterDto dto)
        {
            await _service.ReconstruirSearchText(dto.CodigoPresupuesto);
            return Ok();
        }
   
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAll(AdmLotePagoFilterDto dto)
        {
            var result = await _service.GetAll(dto);
            return Ok(result);
        }
       
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AdmLotePagoUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AdmLotePagoUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AdmLotePagoDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CambioStatus(AdmLotePagoCambioStatusDto dto)
        {
            var result = await _service.CambioStatus(dto);
            return Ok(result);
        }
        
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Aprobar(AdmLotePagoCambioStatusDto dto)
        {
            var result = await _service.Aprobar(dto);
            return Ok(result);
        }

        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Anular(AdmLotePagoCambioStatusDto dto)
        {
            var result = await _service.Anular(dto);
            return Ok(result);
        }
        
   
        
          [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Report(AdmLotePagoReportDto dto)
        {

            var result = "";
            // Ruta donde se guardará el archivo PDF
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.ExcelFiles;
            var filePatch = $"{destino}/{dto.CodigoLotePago}.pdf";
            
        // URL de la API
        string apiUrl = "http://ossmmasoft.com.ve:4000/api-v1.0/payment-orders/pdf/report";
        apiUrl = dto.Report;

        // Token de autenticación (x-refresh-token)
        string refreshToken = "eH6FFBS6Z8jBSMLTvcVHcc/SnGrA4y3pbrR5c76dN1UD+qK91AUv8hH7HJPHni1NEwGkC8IqGNpPDiVAm1ZCYw==";
        dto.Usuario = "OSSMMADEV";
        var sisUsuario = await _sisUsuarioRepository.GetByLogin(dto.Usuario);
        if (sisUsuario != null)
        {
            refreshToken = sisUsuario.REFRESHTOKEN;
        }
        
        // Cuerpo de la solicitud en formato JSON
        var requestBody = new
        {
            codigoLotePago = dto.CodigoLotePago,
            codigoPago = dto.CodigoPago,

        };

        // Convertir el cuerpo a JSON
        string jsonBody = JsonConvert.SerializeObject(requestBody);

        // Crear una instancia de HttpClient
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Configurar el encabezado x-refresh-token
                client.DefaultRequestHeaders.Add("x-refresh-token", refreshToken);
              

                // Configurar el cuerpo de la solicitud y el encabezado Content-Type
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // Realizar la solicitud POST
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                // Verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Leer la respuesta como una cadena
                    /*string responseData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Respuesta exitosa:");
                    Console.WriteLine(responseData);*/
                    
                    
                    // Leer la respuesta como un arreglo de bytes (PDF)
                    byte[] pdfBytes = await response.Content.ReadAsByteArrayAsync();

                  
                   

                    try
                    {
                        
                        // Verifica si el archivo ya existe y lo borra si es necesario.
                        if (System.IO.File.Exists(filePatch))
                        {
                            System.IO.File.Delete(filePatch);
                            Console.WriteLine($"Archivo existente eliminado: {filePatch}");
                        }
                        
                        // Guardar el array de bytes en un archivo PDF
                    
                       System.IO.File.WriteAllBytes(filePatch, pdfBytes);

                        Console.WriteLine("PDF guardado correctamente en: " + filePatch);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al guardar el PDF: " + ex.Message);
                   
                        result="Error al guardar el PDF: " + ex.Message; 
                    }
                    
                    
                   
                    var provider = new FileExtensionContentTypeProvider();
                    if (provider.TryGetContentType(filePatch,out var contenttype))
                    {
                        contenttype = "application/pdf";
                    }
                    var bytes =await System.IO.File.ReadAllBytesAsync(filePatch);
                    var resultFile = File(bytes, contenttype, Path.GetFileName(filePatch));
                    return resultFile;
                    
                    // Devolver el archivo PDF al cliente
                    //return File(pdfBytes, "application/pdf", "report.pdf");
                    
                 
                   
                }
                else
                {
                    var fileName = "NO_DATA.pdf";
                    filePatch = $"{destino}/{fileName}";
                    var provider = new FileExtensionContentTypeProvider();
                    if (provider.TryGetContentType(filePatch,out var contenttype))
                    {
                        contenttype = "application/pdf";
                    }
                    var bytes =await System.IO.File.ReadAllBytesAsync(filePatch);
                    var resultFile = File(bytes, contenttype, Path.GetFileName(filePatch));
                    return resultFile;
                }
            }
            catch (HttpRequestException ex)
            {
                // Manejar errores de red
                Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
                result=$"Error en la solicitud HTTP: {ex.Message}"; 
            }
            catch (Exception ex)
            {
                // Manejar otros errores
                Console.WriteLine($"Error inesperado: {ex.Message}");
                result=$"Error inesperado: {ex.Message}"; 
            }
        }

        
            return Ok(result);

        }

    }
}
