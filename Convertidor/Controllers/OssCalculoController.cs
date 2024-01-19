using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Rh;
using Convertidor.Dtos.Sis;
using Convertidor.Services.Adm;
using Convertidor.Services.Sis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OssCalculoController : ControllerBase
    {
       
        private readonly IOssCalculoService _service;

        public OssCalculoController(IOssCalculoService service)
        {

            _service = service;


        }
        

     
   
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetById(OssCalculoFilterDto dto)
        {
            var result = await _service.GetById(dto);
            return Ok(result);
        }
       
    
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(OssCalculoUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(OssCalculoUpdateDto dto)
        {
            var result = await _service.Create(dto,0);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(OssCalculoDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CalculoTipoNominaPersonaConcepto(RhMovNominaFilterDto dto)
        {
            var result = await _service.CalculoTipoNominaPersonaConcepto(dto);
            return Ok(result);

        }
        
    }
}
