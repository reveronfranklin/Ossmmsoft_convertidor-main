using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteSolicitudModificacion;
using Convertidor.Services.Adm.ReporteSolicitudCompromiso;
using Convertidor.Services.Presupuesto.Reports.ReporteCompromisoPresupuestario;
using Convertidor.Services.Presupuesto.Reports.ReporteSolicitudModificacionPresupuestaria;
using Convertidor.Services.Rh.Report.Example;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ReporteCompromisoPresupuestarioController : ControllerBase
    {
       
        private readonly IReporteCompromisoPresupuestarioService _service;

        public ReporteCompromisoPresupuestarioController(IReporteCompromisoPresupuestarioService service)
        {

            _service = service;
           
        }

       

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ReportData(FilterPreCompromisosDto filter)
        {
          
           var result =await  _service.ReportData(filter);
           return Ok(result);
        }
       

        
    }
}
