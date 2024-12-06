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
    public class AdmImpuestosDocumentosOpController : ControllerBase
    {
       
        private readonly IAdmImpuestosDocumentosOpService _service;

        public AdmImpuestosDocumentosOpController(IAdmImpuestosDocumentosOpService service)
        {

            _service = service;


        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByDocumento(AdmImpuestosDocumentosOpFilterDto dto)
        {
            var result = await _service.GetByDocumento(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AdmImpuestosDocumentosOpUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AdmImpuestosDocumentosOpUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AdmImpuestosDocumentosOpDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
