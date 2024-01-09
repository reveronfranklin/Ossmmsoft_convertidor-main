using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Services.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Rh;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PreRelacionCargosController : ControllerBase
    {
       
        private readonly IPreRelacionCargosService _service;

        public PreRelacionCargosController(IPreRelacionCargosService service)
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
        public async Task<IActionResult> GetAllByPresupuesto(FilterByPresupuestoDto dto)
        {
            var result = await _service.GetAllByPresupuesto(dto);
            return Ok(result);
        }
            
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(PreRelacionCargoUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateField(UpdateFieldDto dto)
        {
            var result = await _service.UpdateField(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(PreRelacionCargoUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(PreRelacionCargoDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }





    }
}
