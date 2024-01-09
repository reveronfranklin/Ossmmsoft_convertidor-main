
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Rh;
using Convertidor.Dtos.Rh;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhTmpRetencionesIncesController : ControllerBase
    {
       
        private readonly IRhTmpRetencionesIncesService _service;

        public RhTmpRetencionesIncesController(IRhTmpRetencionesIncesService service)
        {

            _service = service;
           
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetRetencionesInces(FilterRetencionesDto filter)
        {
                var result = await _service.GetRetencionesInces(filter);
            return Ok(result);
        }

    }
}
