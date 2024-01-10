
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Rh;
using Convertidor.Dtos.Rh;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhHRetencionesFjpController : ControllerBase
    {
       
        private readonly IRhHRetencionesFjpService _service;

        public RhHRetencionesFjpController(IRhHRetencionesFjpService service)
        {

            _service = service;
           
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetRetencionesHFjp(FilterRetencionesDto filter)
        {
                var result = await _service.GetRetencionesHFjp(filter);
            return Ok(result);
        }

    }
}
