﻿using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Sis;
using Convertidor.Services.Sis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SisCuentaBancoController : ControllerBase
    {
       
        private readonly ISisCuentaBancoService _service;

        public SisCuentaBancoController(ISisCuentaBancoService service)
        {

            _service = service;


        }
        
        
   
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAll(SisCuentaBancoFilterDto dto)
        {
            var result = await _service.GetAll(dto);
            return Ok(result);
        }
       
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(SisCuentaBancoUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(SisCuentaBancoUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(SisCuentaBancoDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
