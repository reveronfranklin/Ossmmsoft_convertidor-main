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
    public class AdmDetalleSolicitudController : ControllerBase
    {
       
        private readonly IAdmDetalleSolicitudService _service;

        public AdmDetalleSolicitudController(IAdmDetalleSolicitudService service)
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
        public async Task<IActionResult> GetByCodigoSolicitud(AdmDetalleSolicitudFilterDto filter)
        {
            var result = await _service.GetByCodigoSolicitud(filter.CodigoSolicitud);
            return Ok(result);
        }



        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AdmDetalleSolicitudUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AdmDetalleSolicitudUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AdmDetalleSolicitudDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
