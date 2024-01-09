
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Rh;
using Convertidor.Dtos.Rh;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhHRetencionesCahController : ControllerBase
    {
       
        private readonly IRhHRetencionesCahService _service;

        public RhHRetencionesCahController(IRhHRetencionesCahService service)
        {

            _service = service;
           
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetRetencionesHCah(FilterRetencionesDto filter)
        {
                var result = await _service.GetRetencionesHCah(filter);
            return Ok(result);
        }

    }
}
