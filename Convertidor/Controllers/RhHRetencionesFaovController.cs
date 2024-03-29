﻿
using Microsoft.AspNetCore.Mvc;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhHRetencionesFaovController : ControllerBase
    {
       
        private readonly IRhHRetencionesFaovService _service;

        public RhHRetencionesFaovController(IRhHRetencionesFaovService service)
        {

            _service = service;
           
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetRetencionesHFaov(FilterRetencionesDto filter)
        {
                var result = await _service.GetRetencionesHFaov(filter);
            return Ok(result);
        }

    }
}
