﻿
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Bm;
using Convertidor.Dtos.Bm;
using Convertidor.Dtos.Bm.Mobil;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BmConteoDetalleController : ControllerBase
    {
       
        private readonly IBmConteoDetalleService _service;

        public BmConteoDetalleController(IBmConteoDetalleService service)
        {

            _service = service;
           
        }
        

        [HttpPost]
        [Route("[action]")]

        public async Task<ActionResult> GetAllByConteo(BmConteoFilterDto filter) 
        {
            var result = await _service.GetAllByConteo(filter);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> RecibeConteo(List<ConteoCreateDto> dto) 
        {
            var result = await _service.RecibeConteo(dto);
            return Ok(result);
        }
      
        
        [HttpPost]
        [Route("[action]")]

        public async Task<ActionResult> GetAllByConteoComparar(BmConteoFilterDto filter) 
        {
            var result = await _service.ComparaConteo(filter);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(BmConteoDetalleUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }
      


    }
}
