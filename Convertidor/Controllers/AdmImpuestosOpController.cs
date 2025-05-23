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
    public class AdmImpuestosOpController : ControllerBase
    {
       
        private readonly IAdmImpuestosOpService _service;

        public AdmImpuestosOpController(IAdmImpuestosOpService service)
        {

            _service = service;


        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        
        
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByCodigoOrdenPago(AdmImpuestosOpFilterDto dto)
        {
            var result = await _service.GetByCodigoOrdenPago(dto.CodigoOrdenPago);
            return Ok(result);
        }

            
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AdmImpuestosOpUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AdmImpuestosOpUpdateDto dto)
        {
             var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AdmImpuestosOpDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
