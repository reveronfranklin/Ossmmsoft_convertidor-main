using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Cnt;
using Convertidor.Dtos.Presupuesto;

// HTML to PDF
using Convertidor.Services.Cnt;
using Microsoft.AspNetCore.Mvc;


namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CntHistConciliacionController : ControllerBase
    {

        private readonly ICntHistConciliacionService _service;

        public CntHistConciliacionController(ICntHistConciliacionService service)
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
        public async Task<IActionResult> GetAllByCodigoConciliacion(FilterHistoricoConciliacionDto filter)
        {
            var result = await _service.GetAllByCodigoConciliacion(filter.CodigoConciliacion);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create (CntHistConciliacionUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(CntHistConciliacionUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }
    }
}
