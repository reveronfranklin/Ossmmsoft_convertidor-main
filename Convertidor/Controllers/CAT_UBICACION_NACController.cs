
using Microsoft.AspNetCore.Mvc;
using Convertidor.Dtos.Sis;
using Convertidor.Services.Sis;
using Convertidor.Data.Interfaces.Catastro;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CAT_UBICACION_NACController : ControllerBase
    {
       
        private readonly ICAT_UBICACION_NACRepository _service;

        public CAT_UBICACION_NACController(ICAT_UBICACION_NACRepository service)
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
