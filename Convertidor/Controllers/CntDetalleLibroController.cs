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
    public class CntDetalleLibroController : ControllerBase
    {

        private readonly ICntDetalleLibroService _service;

        public CntDetalleLibroController(ICntDetalleLibroService service)
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
        public async Task<IActionResult> GetAllByCodigoLibro(FilterDetalleLibro filter) 
        {
            var result = await _service.GetAllByCodigoLibro(filter.CodigoLibro);
            return Ok(result);
        
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(CntDetalleLibroUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(CntDetalleLibroUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

    }
}
