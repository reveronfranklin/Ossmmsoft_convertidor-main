
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Rh;
using Convertidor.Dtos.Rh;
using Microsoft.AspNetCore.Authorization;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhTmpRetencionesCahController : ControllerBase
    {
       
        private readonly IRhTmpRetencionesCahService _service;

        public RhTmpRetencionesCahController(IRhTmpRetencionesCahService service)
        {

            _service = service;
           
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetRetencionesCah(FilterRetencionesDto filter)
        {
            var result = await _service.GetRetencionesCah(filter);
            return Ok(result);
        }

    }
}
