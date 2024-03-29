﻿using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Presupuesto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreVDocModificadoController : ControllerBase
    {

        private readonly IPRE_V_DOC_MODIFICADOServices _service;
        private readonly IConfiguration _configuration;

        public PreVDocModificadoController(IPRE_V_DOC_MODIFICADOServices service, IConfiguration configuration)
        {

            _service = service;
            _configuration = configuration;

        }



     
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAllByCodigoSaldo(FilterDocumentosPreVSaldo filter)
        {
            var result = await _service.GetAllByCodigoSaldo(filter);
            return Ok(result);
        }

      

    }
}
