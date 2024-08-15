using Convertidor.Dtos.Catastro;
using Convertidor.Dtos.Cnt;
using Convertidor.Services.Catastro;

// HTML to PDF
using Convertidor.Services.Cnt;
using Microsoft.AspNetCore.Mvc;


namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CatTitulosController : ControllerBase
    {
       
        private readonly ICatTitulosService _service;

        public CatTitulosController(ICatTitulosService service)
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
        public async Task<IActionResult> Create(CatTitulosUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }








    }
}
