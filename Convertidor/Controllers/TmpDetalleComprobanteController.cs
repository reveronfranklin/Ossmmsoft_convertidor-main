﻿using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Cnt;
using Convertidor.Dtos.Presupuesto;

// HTML to PDF
using Convertidor.Services.Cnt;
using Microsoft.AspNetCore.Mvc;


namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TmpDetalleComprobanteController : ControllerBase
    {

        private readonly ITmpDetalleComprobanteService _service;

        public TmpDetalleComprobanteController(ITmpDetalleComprobanteService service)
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
        public async Task<IActionResult> Create(TmpDetalleComprobanteUpdateDto dto)
        {
            var result = await _service.Create(dto);

            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(TmpDetalleComprobanteUpdateDto dto)
        {
            var result = await _service.Update(dto);

            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(TmpDetalleComprobanteDeleteDto dto)
        {
            var result = await _service.Delete(dto);

            return Ok(result);
        }
    }
}
