﻿using Microsoft.AspNetCore.Mvc;

// HTML to PDF

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhReporteNominaHistoricoController : ControllerBase
    {
       
        private readonly IRhReporteNominaHistoricoService _service;

        public RhReporteNominaHistoricoController(IRhReporteNominaHistoricoService service)
        {

            _service = service;
           
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  GetByPeriodoTipoNomina(FilterRepoteNomina filter)
        {
            var result = await _service.GetByPeriodoTipoNomina(filter);
            return Ok(result);
        }
        
        
    }
}
