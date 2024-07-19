using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteSolicitudModificacion;
using Convertidor.Services.Adm.ReporteSolicitudCompromiso;
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
    public class ReporteSolicitudCompromisoController : ControllerBase
    {
       
        private readonly IReporteSolicitudCompromisoService _service;

        public ReporteSolicitudCompromisoController(IReporteSolicitudCompromisoService service)
        {

            _service = service;
           
        }

       

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ReportData(AdmSolicitudesFilterDto filter)
        {
          
           var result =await  _service.ReportData(filter);
           return Ok(result);
        }
       

        
    }
}
