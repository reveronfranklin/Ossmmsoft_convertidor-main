﻿
using Microsoft.AspNetCore.Mvc;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhTmpRetencionesSsoController : ControllerBase
    {
       
        private readonly IRhTmpRetencionesSsoService _service;

        public RhTmpRetencionesSsoController(IRhTmpRetencionesSsoService service)
        {

            _service = service;
           
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetRetencionesSso(FilterRetencionesDto filter)
        {
            var result = await _service.GetRetencionesSso(filter);
            return Ok(result);
        }

    }
}
