﻿using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Adm;
using Convertidor.Services.Adm;
using Convertidor.Services.Adm.AdmDocumentosOp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdmDocumentosOpController : ControllerBase
    {
       
        private readonly IAdmDocumentosOpService _service;

        public AdmDocumentosOpController(IAdmDocumentosOpService service)
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
        public async Task<IActionResult> GetByCodigoOrdenPago(AdmDocumentosFilterDto dto)
        {
            var result = await _service.GetByCodigoOrdenPago(dto);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AdmDocumentosOpUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AdmDocumentosOpUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AdmDocumentosOpDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
