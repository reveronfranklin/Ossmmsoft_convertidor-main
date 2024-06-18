
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Bm;
using Convertidor.Dtos.Bm;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BmDirHBienController : ControllerBase
    {
       
        private readonly IBmDirHBienService _service;

        public BmDirHBienController(IBmDirHBienService service)
        {

            _service = service;
           
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(BmDirHBienUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(BmDirHBienUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(BmDirHBienDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }

    }
}
