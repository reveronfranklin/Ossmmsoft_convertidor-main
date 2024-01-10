
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Rh;
using Convertidor.Dtos.Rh;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhAriController : ControllerBase
    {
       
        private readonly IRhAriService _service;

        public RhAriController(IRhAriService service)
        {

            _service = service;
           
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByPersona(PersonaFilterDto filter)
        {
            var result = await _service.GetByCodigoPersona(filter.CodigoPersona);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(RhAriUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(RhAriUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(RhAriDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }

    }
}
