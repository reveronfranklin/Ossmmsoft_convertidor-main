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
    public class CntReversoConciliacionController : ControllerBase
    {

        private readonly ICntReversoConciliacionService _service;

        public CntReversoConciliacionController(ICntReversoConciliacionService service)
        {

            _service = service;


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
        public async Task<IActionResult> Create(CntReversoConciliacionUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(CntReversoConciliacionUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }
    }
}
