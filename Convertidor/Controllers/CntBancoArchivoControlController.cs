using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Cnt;
using Convertidor.Dtos.Presupuesto;

// HTML to PDF
using Convertidor.Services.Cnt;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CntBancoArchivoControlController : ControllerBase
    {
       
        private readonly ICntBancoArchivoControlService _service;

        public CntBancoArchivoControlController(ICntBancoArchivoControlService service)
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

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(CntBancoArchivoControlUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }
    }
}
