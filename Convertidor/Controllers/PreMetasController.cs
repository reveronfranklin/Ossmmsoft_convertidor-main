﻿using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Presupuesto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PreMetasController : ControllerBase
    {

        private readonly IPreMetasService _service;

        public PreMetasController(IPreMetasService service)
        {

            _service = service;


        }




        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllByPresupuesto(FilterByPresupuestoProyectoDto filter)
        {
            var result = await _service.GetAllByPresupuesto(filter);
            return Ok(result);
        }



        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(PreMetasUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(PreMetasUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(PreMetasDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);


        }
    }
}
