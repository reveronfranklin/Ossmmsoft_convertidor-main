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
using Convertidor.Services.Sis;
using Convertidor.Dtos.Sis;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhRelacionCargosController : ControllerBase
    {
       
        private readonly IRhRelacionCargosService _service;

        public RhRelacionCargosController(IRhRelacionCargosService service)
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
        public async Task<IActionResult> GetAllByRelacionCargo(FilterByPreRelacionCargoDto dto)
        {
            var result = await _service.GetAllByPreRelacionCargo(dto.CodigoRelacionCargoPre);
            return Ok(result);
        }
            
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(RhRelacionCargoUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateField(UpdateFieldDto dto)
        {
            var result = await _service.UpdateField(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(RhRelacionCargoUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(RhRelacionCargoDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }





    }
}
