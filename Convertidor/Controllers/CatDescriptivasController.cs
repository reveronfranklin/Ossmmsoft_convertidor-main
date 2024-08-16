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
    public class CatDescriptivasController : ControllerBase
    {
       
        private readonly ICatDescriptivasService _service;

        public CatDescriptivasController(ICatDescriptivasService service)
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


    }
}
