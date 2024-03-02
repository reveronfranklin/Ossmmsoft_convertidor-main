using Convertidor.Dtos.Bm;
using Convertidor.Services.Bm;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Services.Catastro;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class Bm1Controller : ControllerBase
    {
       
        private readonly IBM_V_BM1Service _service;

        public Bm1Controller(IBM_V_BM1Service service)
        {

            _service = service;


        }

       
     

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]")] 
        public async Task<IActionResult> GetListICP()
        {
            var result = await _service.GetICP();
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")] 
        public async Task<IActionResult> GetByListIcp(List<ICPGetDto>? listIcpSeleccionado)
        {
            var result = await _service.GetByListIcp(listIcpSeleccionado);
            return Ok(result);
        }
    }
}
