
using Microsoft.AspNetCore.Mvc;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhHRetencionesIncesController : ControllerBase
    {
       
        private readonly IRhHRetencionesIncesService _service;

        public RhHRetencionesIncesController(IRhHRetencionesIncesService service)
        {

            _service = service;
           
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetRetencionesHInces(FilterRetencionesDto filter)
        {
                var result = await _service.GetRetencionesHInces(filter);
            return Ok(result);
        }

    }
}
