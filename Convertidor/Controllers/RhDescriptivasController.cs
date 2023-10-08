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
using Convertidor.Dtos.Rh;
using Microsoft.AspNetCore.Authorization;
using Convertidor.Services.Sis;
using Convertidor.Dtos.Sis;
using Convertidor.Services.Rh;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhDescriptivasController : ControllerBase
    {
       
        private readonly IRhDescriptivasService _service;

        public RhDescriptivasController(IRhDescriptivasService service)
        {

            _service = service;


        }


   
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByTitulo(FilterRhDescriptiva dto)
        {
            var result = await _service.GetByTitulo(dto.TituloId);
            return Ok(result);
        }
 


    }
}
