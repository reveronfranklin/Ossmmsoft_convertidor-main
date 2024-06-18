
using Microsoft.AspNetCore.Mvc;
using Convertidor.Dtos.Sis;
using Convertidor.Services.Sis;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SisUbicacionController : ControllerBase
    {
       
        private readonly ISisUbicacionService _service;

        public SisUbicacionController(ISisUbicacionService service)
        {

            _service = service;
           
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetPaises(FilterClave filter)
        {
                var result = await _service.GetPaises();
            return Ok(result);
        }
        
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetEstados(FilterClave filter)
        {
            var result = await _service.GetEstados();
            return Ok(result);
        }
      
    }
}
