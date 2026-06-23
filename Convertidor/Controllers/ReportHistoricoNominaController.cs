
using Convertidor.Services.Rh.Report.HistoricoNomina;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ReportHistoricoNominaController : ControllerBase
    {
       
        private readonly IReportHistoricoNominaService _service;

        public ReportHistoricoNominaController(IReportHistoricoNominaService service)
        {

            _service = service;
           
        }
        
        [HttpPost]
        [Route("[action]")]
        [Obsolete("ReporteGeneralNomina fue migrado a OssmmasoftVerticalSlice: POST /api/ReporteGeneralNomina/pdf. Mantener temporalmente solo por compatibilidad.")]
        public async Task<IActionResult>  GeneratePdf(FilterRepoteNomina filter)
        {
           var result =await  _service.GeneratePdf(filter);
           return Ok(result);
        }
       

        
    }
}
