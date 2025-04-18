﻿
using Microsoft.AspNetCore.Mvc;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhDocumentosDetallesController : ControllerBase
    {
       
        private readonly IRhDocumentosDetallesService _service;

        public RhDocumentosDetallesController(IRhDocumentosDetallesService service)
        {

            _service = service;
           
        }

        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByDocumento(FilterDocumentoDetalleDto dto)
        {
            var result = await _service.GetByDocumento(dto.CodigoDocumento);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(RhDocumentosDetallesUpdate dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(RhDocumentosDetallesUpdate dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(RhDocumentosDetallesDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }

    }
}
