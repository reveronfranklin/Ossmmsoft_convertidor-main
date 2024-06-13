using Convertidor.Dtos.Adm;
using Convertidor.Services.Adm.Reports.ReporteSolicitudCompromiso;
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
        public async Task<IActionResult> ReportData(FilterSolicitudCompromisoDto filter)
        {
          
           var result = await  _service.ReportData(filter);
           return Ok(result);
        }
       

        
    }
}
