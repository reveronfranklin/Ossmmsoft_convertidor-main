
using Convertidor.Dtos.Adm;
using Convertidor.Services.Adm.ReporteSolicitudCompromiso;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

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
        private readonly IConfiguration _configuration;

        public ReporteSolicitudCompromisoController(
            IReporteSolicitudCompromisoService service,
            IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
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
