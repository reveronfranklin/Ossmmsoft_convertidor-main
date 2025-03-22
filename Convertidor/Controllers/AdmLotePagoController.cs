using Convertidor.Dtos.Adm;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Sis;
using Convertidor.Services.Adm;
using Convertidor.Services.Sis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdmLotePagoController : ControllerBase
    {
       
        private readonly IAdmLotePagoService _service;

        public AdmLotePagoController(IAdmLotePagoService service)
        {

            _service = service;


        }
        
        
   
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAll(AdmLotePagoFilterDto dto)
        {
            var result = await _service.GetAll(dto);
            return Ok(result);
        }
       
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AdmLotePagoUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AdmLotePagoUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AdmLotePagoDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CambioStatus(AdmLotePagoCambioStatusDto dto)
        {
            var result = await _service.CambioStatus(dto);
            return Ok(result);
        }
        

    }
}
