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
    public class AdmBeneficiariosOpController : ControllerBase
    {
       
        private readonly IAdmBeneficariosOpService _service;

        public AdmBeneficiariosOpController(IAdmBeneficariosOpService service)
        {

            _service = service;


        }

        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByOrdenPago(AdmOrdenPagoBeneficiarioFlterDto dto)
        {
            var result = await _service.GetByOrdenPago(dto);
            return Ok(result);
        }
       
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AdmBeneficiariosOpUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AdmBeneficiariosOpUpdateDto dto)
        {
             var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AdmBeneficiariosOpDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
