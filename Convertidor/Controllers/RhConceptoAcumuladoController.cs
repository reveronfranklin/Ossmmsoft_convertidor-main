using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Adm;
using Convertidor.Services.Adm;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhConceptoAcumuladoController : ControllerBase
    {
       
        private readonly IRhConceptosAcumuladoService _service;

        public RhConceptoAcumuladoController(IRhConceptosAcumuladoService service)
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
        public async Task<IActionResult> GetAllByConcepto(RhConceptosAcumulaFilterDto dto)
        {
            var result = await _service.GetAllByConcepto(dto);
            return Ok(result);
        }
   
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByCodigo(RhConceptosAcumulaFilterDto dto)
        {
            var result = await _service.GetByCodigo(dto);
            return Ok(result);
        }
        

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(RhConceptosAcumulaUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(RhConceptosAcumulaUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(RhConceptosAcumulaDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
