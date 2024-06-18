using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Presupuesto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreVDocBloqueadoController : ControllerBase
    {

        private readonly IPRE_V_DOC_BLOQUEADOServices _service;
        private readonly IConfiguration _configuration;

        public PreVDocBloqueadoController(IPRE_V_DOC_BLOQUEADOServices service, IConfiguration configuration)
        {

            _service = service;
            _configuration = configuration;

        }



     
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAllByCodigoSaldo(FilterDocumentosPreVSaldo filter)
        {
            var result = await _service.GetAllByCodigoSaldo(filter);
            return Ok(result);
        }

      

    }
}
