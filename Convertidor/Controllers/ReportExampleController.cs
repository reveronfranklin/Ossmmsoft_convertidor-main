using Convertidor.Services.Rh.Report.Example;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ReportExampleController : ControllerBase
    {
       
        private readonly IReportExampleService _service;

        public ReportExampleController(IReportExampleService service)
        {

            _service = service;
           
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult>  GeneratePdf()
        {
            _service.GeneratePdf();
            return Ok();
        }
       

        
    }
}
