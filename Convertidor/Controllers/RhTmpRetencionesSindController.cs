﻿
using Microsoft.AspNetCore.Mvc;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhTmpRetencionesSindController : ControllerBase
    {
       
        private readonly IRhTmpRetencionesSindService _service;

        public RhTmpRetencionesSindController(IRhTmpRetencionesSindService service)
        {

            _service = service;
           
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetRetencionesSind(FilterRetencionesDto filter)
        {
                var result = await _service.GetRetencionesSind(filter);
            return Ok(result);
        }

    }
}
