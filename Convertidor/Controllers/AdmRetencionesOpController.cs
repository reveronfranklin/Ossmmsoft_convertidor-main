using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Adm;
using Convertidor.Services.Adm;
using Convertidor.Services.Adm.AdmRetencionesOp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdmRetencionesOpController : ControllerBase
    {
       
        private readonly IAdmRetencionesOpService _service;

        public AdmRetencionesOpController(IAdmRetencionesOpService service)
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
        public async Task<IActionResult> GetByOrdenPago(AdmRetencionesFilterDto filter)
        {
            var result = await _service.GetByOrdenPago(filter);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AdmRetencionesOpUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AdmRetencionesOpUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AdmRetencionesOpDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
