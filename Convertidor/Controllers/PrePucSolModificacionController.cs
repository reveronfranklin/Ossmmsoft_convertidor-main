using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Presupuesto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PrePucSolModificacionController : ControllerBase
    {
       
        private readonly IPrePucSolicitudModificacionService _service;

        public PrePucSolModificacionController(IPrePucSolicitudModificacionService service)
        {

            _service = service;


        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  GetAllByCodigoSolicitud(PrePucSolModificacionFilterDto filter)
        {
            var result = await _service.GetAllByCodigoSolicitud(filter);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(PrePucSolModificacionUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(PrePucSolModificacionUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(PrePucSolModificacionDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }


    }
}
