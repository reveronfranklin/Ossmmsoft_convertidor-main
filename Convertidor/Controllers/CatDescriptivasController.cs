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

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(CatDescriptivasUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(CatDescriptivasUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(CatDescriptivasDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }


    }
}
