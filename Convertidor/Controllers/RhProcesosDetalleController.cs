using Convertidor.Dtos.Rh;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Services.Rh;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhProcesosDetalleController : ControllerBase
    {
       
        private readonly IRhProcesosDetalleService _service;

        public RhProcesosDetalleController(IRhProcesosDetalleService service)
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
        public async Task<IActionResult> GetByCodigo(RhProcesosDetalleFilterDtoDto filter)
        {
            var result = await _service.GetByCodigo(filter.CodigoDetalleProceso);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByProceso(RhProcesosDetalleFilterDtoDto filter)
        {
            var result = await _service.GetByProceso(filter);
            return Ok(result);
        }
        
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(RhProcesosDetalleUpdateDtoDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(RhProcesosDetalleUpdateDtoDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(RhProcesosDetalleDeleteDtoDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }
        


    }
}
