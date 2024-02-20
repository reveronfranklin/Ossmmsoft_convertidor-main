
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Convertidor.Services.Bm;
using Convertidor.Dtos.Bm;
using Convertidor.Data.Entities.Bm;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BmMovBienesController : ControllerBase
    {
       
        private readonly IBmMovBienesService _service;

        public BmMovBienesController(IBmMovBienesService service)
        {

            _service = service;
           
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(BmMovBienesUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(BmMovBienesUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(BmMovBienesDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }

    }
}
