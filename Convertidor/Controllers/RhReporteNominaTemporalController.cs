using Microsoft.AspNetCore.Mvc;

// HTML to PDF

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhReporteNominaTemporalController : ControllerBase
    {
       
        private readonly IRhReporteNominaTemporalService _service;

        public RhReporteNominaTemporalController(IRhReporteNominaTemporalService service)
        {

            _service = service;
           
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  GetByPeriodoTipoNomina(FilterRepoteNomina filter)
        {
            var result = await _service.GetByPeriodoTipoNomina(filter);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  GetByPeriodoTipoNominaPersona(FilterRepoteNomina filter)
        {
            var result = await _service.GetByPeriodoTipoNominaPersona(filter);
            return Ok(result);
        }
        
    }
}
