using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Rh.Report.Example;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ReportPreResumenSaldoController : ControllerBase
    {
       
        private readonly IReportPreResumenSaldoService _service;

        public ReportPreResumenSaldoController(IReportPreResumenSaldoService service)
        {

            _service = service;
           
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  GeneratePdf(FilterResumenSaldo filter)
        {
           var result =await  _service.GeneratePdf(filter);
           return Ok(result);
        }
       

        
    }
}
