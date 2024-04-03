using Microsoft.AspNetCore.Mvc;

// HTML to PDF

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhVReciboPagoController : ControllerBase
    {
       
        private readonly IRhVReciboPagoService _service;

        public RhVReciboPagoController(IRhVReciboPagoService service)
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
        
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetByFilter(FilterReciboPagoDto dto)
        {
            var result = await _service.GetByFilter(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateReportReciboPago(FilterReciboPagoDto dto)
        {
            await _service.CreateReportReciboPago(dto.CodigoPeriodo, dto.CodigoTipoNomina, dto.CodigoPersona);
            return Ok();
        }



    }
}
