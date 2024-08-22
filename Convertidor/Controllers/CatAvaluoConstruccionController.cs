﻿using Convertidor.Dtos.Catastro;
using Convertidor.Services.Catastro;

// HTML to PDF
using Microsoft.AspNetCore.Mvc;


namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CatAvaluoConstruccionController : ControllerBase
    {
       
        private readonly ICatAvaluoConstruccionService _service;

        public CatAvaluoConstruccionController(ICatAvaluoConstruccionService service)
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

        
    }
}
