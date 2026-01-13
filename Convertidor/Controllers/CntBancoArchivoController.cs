using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Cnt;
using Convertidor.Dtos.Presupuesto;

// HTML to PDF
using Convertidor.Services.Cnt;
using Microsoft.AspNetCore.Mvc;


namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CntBancoArchivoController : ControllerBase
    {
       
        private readonly ICntBancoArchivoService _service;

        public CntBancoArchivoController(ICntBancoArchivoService service)
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
        public async Task<IActionResult> Create(CntBancoArchivoUpdateDto dto)
        {
            var result = await _service.Create(dto); 
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(CntBancoArchivoUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(CntBancoArchivoDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }
    }
}
