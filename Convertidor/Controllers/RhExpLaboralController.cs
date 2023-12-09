
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Rh;
using Convertidor.Dtos.Rh;
using Microsoft.AspNetCore.Authorization;
using Convertidor.Data.Entities.Rh;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhExpLaboralController : ControllerBase
    {
       
        private readonly IRhExpLaboralService _service;

        public RhExpLaboralController(IRhExpLaboralService service)
        {

            _service = service;
           
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByPersona(PersonaFilterDto persona)
        {
            var result = await _service.GetByCodigoPersona(persona.CodigoPersona);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(RhExpLaboralUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(RhExpLaboralUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(RhExpLaboralDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }

    }
}
