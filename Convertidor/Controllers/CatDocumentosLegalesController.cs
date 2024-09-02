using Convertidor.Dtos.Catastro;
using Convertidor.Services.Catastro;

// HTML to PDF
using Microsoft.AspNetCore.Mvc;


namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CatDocumentosLegalesController : ControllerBase
    {
       
        private readonly ICatDocumentosLegalesService _service;

        public CatDocumentosLegalesController(ICatDocumentosLegalesService service)
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
        public async Task<IActionResult> Create(CatDocumentosLegalesUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

    }
}
