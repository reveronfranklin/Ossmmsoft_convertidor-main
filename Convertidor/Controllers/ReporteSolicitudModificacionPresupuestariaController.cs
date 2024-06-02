using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteSolicitudModificacion;
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
    public class ReporteSolicitudModificacionPresupuestariaController : ControllerBase
    {
       
        private readonly IReporteSolicitudModificacionPresupuestariaService _service;

        public ReporteSolicitudModificacionPresupuestariaController(IReporteSolicitudModificacionPresupuestariaService service)
        {

            _service = service;
           
        }

       

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ReportData(FilterDto filter)
        {
          
           var result =await  _service.ReportData(filter.CodigoSolModificacion,filter.DePara);
           return Ok(result);
        }
       

        
    }
}
