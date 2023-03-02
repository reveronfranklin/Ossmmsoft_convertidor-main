using AppService.Api.Utility;
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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreVSaldosController : ControllerBase
    {
       
        private readonly IPRE_V_SALDOSServices _service;

        public PreVSaldosController(IPRE_V_SALDOSServices service)
        {

            _service = service;
           
        }



       
       
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  GetAll(FilterPRE_V_SALDOSDto filter)
        {
            var result = await _service.GetAll(filter);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAllByPresupuesto(FilterPRE_V_SALDOSDto filter)
        {
            var result = await _service.GetAll(filter);
            return Ok(result);
        }


    }
}
