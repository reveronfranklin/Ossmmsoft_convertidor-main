
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Rh;
using Convertidor.Dtos.Rh;
using Microsoft.AspNetCore.Authorization;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhHRetencionesSindController : ControllerBase
    {
       
        private readonly IRhHRetencionesSindService _service;

        public RhHRetencionesSindController(IRhHRetencionesSindService service)
        {

            _service = service;
           
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetRetencionesHSind(FilterRetencionesDto filter)
        {
                var result = await _service.GetRetencionesHSind(filter);
            return Ok(result);
        }

    }
}
