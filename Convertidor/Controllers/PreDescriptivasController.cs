﻿using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Presupuesto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PreDescriptivasController : ControllerBase
    {
       
        private readonly IPreDescriptivasService _service;

        public PreDescriptivasController(IPreDescriptivasService service)
        {

            _service = service;


        }

       
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetTreeDescriptiva()
        {
                var result = await _service.GetTreeDecriptiva();
                return Ok(result);
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
        public async Task<IActionResult> GetAllByTitulo(FilterPreTituloDto dto)
        {
            var result = await _service.GetByTitulo(dto.TituloId);
            return Ok(result);
        }
        
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAllByCodigoTitulo(FilterPreTituloDto dto)
        {
            var result = await _service.GetByCodigoTitulo(dto.Codigo);
            return Ok(result);
        }

        
   

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(PreDescriptivasUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(PreDescriptivasUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(PreDescriptivaDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
