using Microsoft.AspNetCore.Mvc;

// HTML to PDF

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhProcesosController : ControllerBase
    {
       
        private readonly IRhProcesosService _service;

        public RhProcesosController(IRhProcesosService service)
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
      
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllRhProcesoResponseDto( )
        {
            var result = await _service.GetAllRhProcesoResponseDto();
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByCodigo(RhProcesosFilterDtoDto filter)
        {
            var result = await _service.GetByCodigo(filter.CodigoProceso);
            return Ok(result);
        }
        
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByProceso(RhProcesosFilterDtoDto filter)
        {
            var result = await _service.GetByProceso(filter);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(RhProcesosUpdateDtoDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(RhProcesosUpdateDtoDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(RhProcesosDeleteDtoDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }
        


    }
}
