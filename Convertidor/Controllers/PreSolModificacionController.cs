using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Presupuesto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PreSolModificacionController : ControllerBase
    {
       
        private readonly IPreSolModificacionService _service;

        public PreSolModificacionController(IPreSolModificacionService service)
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
        public async Task<IActionResult> GetByPresupuesto(FilterPresupuestoDto dto)
        {
            var result = await _service.GetByPresupuesto(dto);
            return Ok(result);
        }

       

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(PreSolModificacionUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(PreSolModificacionUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(PreSolModificacionDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Aprobar(PreSolModificacionDeleteDto dto)
        {
            var result = await _service.Aprobar(dto);
            return Ok(result);
        }

    }
}
