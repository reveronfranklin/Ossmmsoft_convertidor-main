﻿using AppService.Api.Utility;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Convertidor.Data.Entities;
using Convertidor.Data.Interfaces;
using Convertidor.Services;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using IronPdf;
using Convertidor.Dtos;
using Convertidor.Services.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Microsoft.AspNetCore.Authorization;
using Convertidor.Services.Rh;
using Convertidor.Dtos.Rh;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhTipoNominaController : ControllerBase
    {
       
        private readonly IRhTipoNominaService _service;

        public RhTipoNominaController(IRhTipoNominaService service)
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
        public async Task<IActionResult> GetTipoNominaByCodigoPersona(PersonaFilterDto filter)
        {
            var result = await _service.GetTipoNominaByCodigoPersona(filter.CodigoPersona,(DateTime)filter.Desde,(DateTime)filter.Hasta);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(RhTiposNominaUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(RhTiposNominaUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(RhTiposNominaDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }



    }
}
