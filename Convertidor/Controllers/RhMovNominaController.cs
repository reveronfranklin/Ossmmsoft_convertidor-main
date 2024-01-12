using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Adm;
using Convertidor.Services.Rh;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhMovNominaController : ControllerBase
    {
       
        private readonly IRhMovNominaService _service;

        public RhMovNominaController(IRhMovNominaService service)
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
        public async Task<IActionResult> GetAllByCodigo(RhMovNominaFilterDto dto)
        {
            var result = await _service.GetByCodigo(dto);
            return Ok(result);
        }
   
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByPersona(RhMovNominaFilterDto dto)
        {
            var result = await _service.GetAllByPersona(dto);
            return Ok(result);
        }
        

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(RhMovNominaUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(RhMovNominaUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(RhMovNominaDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
