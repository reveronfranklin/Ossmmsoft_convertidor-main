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
using Microsoft.AspNetCore.Authorization;
using Convertidor.Services.Rh;
using Convertidor.Dtos.Rh;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhHistoricoPersonalCargoController : ControllerBase
    {
       
        private readonly IHistoricoPersonalCargoService _service;

        public RhHistoricoPersonalCargoController(IHistoricoPersonalCargoService service)
        {

            _service = service;
           
        }

        

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetListCargosPorPersona(PersonaFilterDto filter)
        {
            var result = await _service.GetListCargosPorPersona(filter.CodigoPersona);
            return Ok(result);
        }



    }
}
