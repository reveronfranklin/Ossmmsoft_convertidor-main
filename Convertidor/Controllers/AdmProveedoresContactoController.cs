﻿using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Adm;
using Convertidor.Services.Adm;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdmProveedoresContactoController : ControllerBase
    {
       
        private readonly IAdmProveedoresContactoService _service;

        public AdmProveedoresContactoController(IAdmProveedoresContactoService service)
        {

            _service = service;


        }
        

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAll(AdmProveedorContactoFilterDto dto)
        {
            var result = await _service.GetAll(dto);
            return Ok(result);
        }

   
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByCodigo(AdmProveedorContactoFilterDto dto)
        {
            var result = await _service.GetByCodigo(dto);
            return Ok(result);
        }
        

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AdmProveedorContactoUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AdmProveedorContactoUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AdmProveedorContactoDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
