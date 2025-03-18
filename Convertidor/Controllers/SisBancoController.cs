using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Sis;
using Convertidor.Services.Sis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SisBancoController : ControllerBase
    {
       
        private readonly ISisBancoService _service;

        public SisBancoController(ISisBancoService service)
        {

            _service = service;


        }
        
        
   
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAll(SisBancoFilterDto dto)
        {
            var result = await _service.GetAll(dto);
            return Ok(result);
        }
       
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(SisBancoUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(SisBancoUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(SisBancoDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
