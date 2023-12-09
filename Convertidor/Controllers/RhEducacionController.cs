
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Rh;
using Convertidor.Dtos.Rh;
using Microsoft.AspNetCore.Authorization;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhEducacionController : ControllerBase
    {
       
        private readonly IRhEducacionService _service;

        public RhEducacionController(IRhEducacionService service)
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
        public async Task<IActionResult> Create(RhEducacionUpdate dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(RhEducacionUpdate dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(RhEducacionDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }

    }
}
