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
    public class AdmOrdenPagoController : ControllerBase
    {
       
        private readonly IAdmOrdenPagoService _service;

        public AdmOrdenPagoController(IAdmOrdenPagoService service)
        {

            _service = service;


        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByPresupuesto(AdmOrdenPagoFilterDto filter)
        {
            var result = await _service.GetByPresupuesto(filter);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AdmOrdenPagoUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AdmOrdenPagoUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AdmOrdenPagoDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
