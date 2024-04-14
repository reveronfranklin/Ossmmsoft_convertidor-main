using Microsoft.AspNetCore.Mvc;

// HTML to PDF

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhReporteNominaHistoricoController : ControllerBase
    {
       
        private readonly IRhReporteNominaHistoricoService _service;
        private readonly IRhReporteNominaTemporalService _serviceTemporal;

        public RhReporteNominaHistoricoController(IRhReporteNominaHistoricoService service,IRhReporteNominaTemporalService serviceTemporal)
        {
            _service = service;
            _serviceTemporal = serviceTemporal;
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
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  GetByPeriodoTipoNominaResumen(FilterRepoteNomina filter)
        {
            var temporal = await _serviceTemporal.GetByPeriodoTipoNominaResumen(filter);
            if (temporal.Data!=null && temporal.Data.Count > 0)
            {
                if (filter.CodigoIcp!= null && filter.CodigoIcp > 0)
                {
                    temporal.Data = temporal.Data.Where(x => x.CodigoIcp == filter.CodigoIcp).ToList();
                }
                return Ok(temporal);
            }
            else
            {
                var historico = await _service.GetByPeriodoTipoNominaResumen(filter);
                if (filter.CodigoIcp!= null && filter.CodigoIcp > 0)
                {
                    historico.Data = historico.Data.Where(x => x.CodigoIcp == filter.CodigoIcp).ToList();
                }
                return Ok(historico);
            }
            
           
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  ListIcp(FilterRepoteNomina filter)
        {
            var temporal = await _serviceTemporal.GetByPeriodoTipoNominaResumen(filter);
            if (temporal.Data!=null && temporal.Data.Count > 0)
            {
                var data = await _serviceTemporal.ListIcp(filter);
                return Ok(temporal);
            }
            else
            {
                var historico = await _service.ListIcp(filter);
                return Ok(historico);
            }
            
           
        }
        

        
    }
}
