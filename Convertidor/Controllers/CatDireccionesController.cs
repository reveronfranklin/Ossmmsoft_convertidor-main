using Convertidor.Dtos.Catastro;
using Convertidor.Services.Catastro;

// HTML to PDF
using Microsoft.AspNetCore.Mvc;


namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CatDireccionesController : ControllerBase
    {
       
        private readonly ICatDireccionesService _service;

        public CatDireccionesController(ICatDireccionesService service)
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
        public async Task<IActionResult> Create(CatDireccionesUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }


    }
}
