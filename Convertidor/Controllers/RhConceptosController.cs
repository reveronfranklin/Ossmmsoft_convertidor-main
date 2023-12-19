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
    public class RhConceptosController : ControllerBase
    {
       
        private readonly IRhConceptosService _service;

        public RhConceptosController(IRhConceptosService service)
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
        public async Task<IActionResult> GetConceptosByPersonas(PersonaFilterDto filter)
        {
            var result = await _service.GetConceptosByCodigoPersona(filter.CodigoPersona,(DateTime)filter.Desde,(DateTime)filter.Hasta);
            return Ok(result.OrderBy(x=>x.Denominacion));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(RhConceptosUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(RhConceptosUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        //[HttpPost]
        //[Route("[action]")]
        //public async Task<IActionResult> Delete(RhConceptosDeleteDto dto)
        //{
        //    var result = await _service.Delete(dto);
        //    return Ok(result);
        //}

    }
}
