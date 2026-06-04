using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.StaticFiles;
using System.Text;
// HTML to PDF

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhMovNominaController : ControllerBase
    {
       
        private readonly IRhMovNominaService _service;
        private readonly IConfiguration _configuration;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public RhMovNominaController(
            IRhMovNominaService service,
            IConfiguration configuration,
              ISisUsuarioRepository sisUsuarioRepository)
        {

            _service = service;
            _configuration = configuration;
            _sisUsuarioRepository = sisUsuarioRepository;

        }
        

        

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAllByCodigo(RhMovNominaFilterDto dto)
        {
            var result = await _service.GetByCodigo(dto);
            return Ok(result);
        }
   
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByPersona(RhMovNominaFilterDto dto)
        {
            var result = await _service.GetAllByPersona(dto);
            return Ok(result);
        }
        

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(RhMovNominaUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(RhMovNominaUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(RhMovNominaDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Report(RhMovNominaReportDto dto)
        {

            var result = "";
            // Ruta donde se guardará el archivo PDF
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.ExcelFiles;
            var filePatch = $"{destino}{@settings.SeparatorPatch}{dto.CodigoTipoNomina}.pdf";
            int empresa = Convert.ToInt32(settings.EmpresaConfig);
            // URL de la API
            string apiUrl =@settings.ApiUrlReport; //"http://ossmmasoft.com.ve:4000/api-v1.0/payment-orders/pdf/report";
            apiUrl = dto.Report;

            // Token de autenticación (x-refresh-token)
            string refreshToken =@settings.RefreshTokenReport; //"eH6FFBS6Z8jBSMLTvcVHcc/SnGrA4y3pbrR5c76dN1UD+qK91AUv8hH7HJPHni1NEwGkC8IqGNpPDiVAm1ZCYw==";
            dto.Usuario =@settings.UsuarioReport; //"OSSMMADEV";
            var sisUsuario = await _sisUsuarioRepository.GetByLogin(dto.Usuario);
            if (sisUsuario != null)
            {
                refreshToken = sisUsuario.REFRESHTOKEN;
            }
            string paymentDate = dto.FechaPago.ToString("yyyy-MM-dd");
            // Cuerpo de la solicitud en formato JSON
            var requestBody = new
            {
                payrollType = dto.CodigoTipoNomina,
                companyCode=empresa,
                paymentDate=paymentDate,
                generationType=dto.TipoOperacion,
                periodCode=dto.CodigoPeriodo,
                idCard=dto.Cedula
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
