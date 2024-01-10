
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Rh;
using Convertidor.Dtos.Rh;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhHRetencionesSsoController : ControllerBase
    {
       
        private readonly IRhHRetencionesSsoService _service;

        public RhHRetencionesSsoController(IRhHRetencionesSsoService service)
        {

            _service = service;
           
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetRetencionesHSso(FilterRetencionesDto filter)
        {
                var result = await _service.GetRetencionesHSso(filter);
            return Ok(result);
        }

    }
}
