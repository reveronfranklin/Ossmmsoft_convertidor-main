
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Rh;
using Convertidor.Dtos.Rh;
using Microsoft.AspNetCore.Authorization;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RhComunicacionesController : ControllerBase
    {
       
        private readonly IRhComunicacionService _service;

        public RhComunicacionesController(IRhComunicacionService service)
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
        public async Task<IActionResult> Create(RhComunicacionUpdate dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(RhComunicacionUpdate dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(RhComunicacionDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }

    }
}
