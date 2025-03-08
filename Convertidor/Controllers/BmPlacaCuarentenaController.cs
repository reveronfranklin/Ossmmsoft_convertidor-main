
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Bm;
using Convertidor.Dtos.Bm;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BmPlacaCuarentenaController : ControllerBase
    {
       
        private readonly IBmPlacasCuarentenaService _service;

        public BmPlacaCuarentenaController(IBmPlacasCuarentenaService service)
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
        public async Task<IActionResult> GetByCodigo(BmPlacaCuarentenaFilterDto dto)
        {
                var result = await _service.GetByCodigo(dto.CodigoPlacaCuarentena);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(BmPlacaCuarentenaUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }



        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(BmPlacaCuarentenaDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }

    }
}
