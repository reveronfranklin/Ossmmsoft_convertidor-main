﻿
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Rh;
using Convertidor.Dtos.Rh;
using Microsoft.AspNetCore.Authorization;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhTmpRetencionesFaovController : ControllerBase
    {
       
        private readonly IRhTmpRetencionesFaovService _service;

        public RhTmpRetencionesFaovController(IRhTmpRetencionesFaovService service)
        {

            _service = service;
           
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetRetencionesFaov(FilterRetencionesDto filter)
        {
                var result = await _service.GetRetencionesFaov(filter);
            return Ok(result);
        }

    }
}
