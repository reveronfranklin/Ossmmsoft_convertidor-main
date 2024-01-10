
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Rh;
using Convertidor.Dtos.Rh;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhTmpRetencionesFjpController : ControllerBase
    {
       
        private readonly IRhTmpRetencionesFjpService _service;

        public RhTmpRetencionesFjpController(IRhTmpRetencionesFjpService service)
        {

            _service = service;
           
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetRetencionesFjp(FilterRetencionesDto filter)
        {
                var result = await _service.GetRetencionesFjp(filter);
            return Ok(result);
        }

    }
}
