
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Convertidor.Services.Bm;
using Convertidor.Dtos.Bm;
using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PreAsignacionesDetalleController : ControllerBase
    {
       
        private readonly IPreAsignacionDetalleService _service;

        public PreAsignacionesDetalleController(IPreAsignacionDetalleService service)
        {

            _service = service;
           
        }
        

      
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> GetAllByAsignacion(PreAsignacionesDetalleFilterDto filter)
        {
            var result = await _service.GetAllByAsignacion(filter);
            return Ok(result);
        }
      
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(PreAsignacionesDetalleUpdateDto dto)
        {
            var result = await _service.Add(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(PreAsignacionesDetalleUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(PreAsignacionesDetalleDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }

    }
}
