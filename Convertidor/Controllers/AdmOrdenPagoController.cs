using System.Text;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Adm;
using Convertidor.Services.Adm;
using Convertidor.Services.Destino.ADM;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdmOrdenPagoController : ControllerBase
    {
       
        private readonly IAdmOrdenPagoService _service;
        private readonly IAdmOrdenPagoDestinoService _destinoService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public AdmOrdenPagoController(IAdmOrdenPagoService service,
            IAdmOrdenPagoDestinoService destinoService,
            ISisUsuarioRepository sisUsuarioRepository)
        {
            _service = service;
            _destinoService = destinoService;
            _sisUsuarioRepository = sisUsuarioRepository;
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByPresupuesto(AdmOrdenPagoFilterDto filter)
        {
                var result = await _service.GetByPresupuesto(filter);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AdmOrdenPagoUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Aprobar(AdmOrdenPagoAprobarAnular dto)
        {
            var result = await _service.Aprobar(dto);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Anular(AdmOrdenPagoAprobarAnular dto)
        {
            var result = await _service.Anular(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AdmOrdenPagoUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AdmOrdenPagoDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Replicar(AdmOrdenPagoDeleteDto filter)
        {
            var result = await _destinoService.CopiarOrdenPago(filter.CodigoOrdenPago);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Report(AdmOrdenPagoReportDto dto)
        {

        // URL de la API
        string apiUrl = "http://ossmmasoft.com.ve:4000/api-v1.0/payment-orders/pdf/report";
        apiUrl = dto.Report;

        // Token de autenticación (x-refresh-token)
        string refreshToken = "eH6FFBS6Z8jBSMLTvcVHcc/SnGrA4y3pbrR5c76dN1UD+qK91AUv8hH7HJPHni1NEwGkC8IqGNpPDiVAm1ZCYw==";

        var conectado = await  _sisUsuarioRepository.GetConectado();
        if (!String.IsNullOrEmpty(conectado.RefreshToken))
        {
             refreshToken = conectado.RefreshToken;
        }
        else
        {
            var sisUsuario = await _sisUsuarioRepository.GetByLogin(dto.Usuario);
            if (sisUsuario != null)
            {
                refreshToken = sisUsuario.REFRESHTOKEN;
            }
        }
        
        // Cuerpo de la solicitud en formato JSON
        var requestBody = new
        {
            CodigoOrdenPago = dto.CodigoOrdenPago,
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

                    // Devolver el archivo PDF al cliente
                    return File(pdfBytes, "application/pdf", "report.pdf");
                }
                else
                {
                    // Mostrar el código de estado en caso de error
                    Console.WriteLine($"Error: {response.StatusCode}");
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Detalles del error:");
                    Console.WriteLine(errorResponse);
                }
            }
            catch (HttpRequestException ex)
            {
                // Manejar errores de red
                Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Manejar otros errores
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }
        }

            var result = "";
            return Ok(result);

        }

    }
}
