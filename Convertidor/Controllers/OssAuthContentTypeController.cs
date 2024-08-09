
using Microsoft.AspNetCore.Mvc;
using Convertidor.Dtos.Sis;
using Convertidor.Services.Sis;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OssAuthContentTypeController : ControllerBase
    {
        private readonly OssAuthContentTypeService _service;


        public OssAuthContentTypeController(OssAuthContentTypeService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
                var result = await _service.GetAll();
            return Ok(result);
        }
        
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetById(AuthContentTypeFilterDto filter)
        {
            var result = await _service.GetById(filter);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AuthContentTypeUpdateDto filter)
        {
            var result = await _service.Update(filter);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AuthContentTypeUpdateDto filter)
        {
            var result = await _service.Create(filter);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AuthContentTypeDeleteDto filter)
        {
            var result = await _service.Delete(filter);
            return Ok(result);
        }
      
    }
}
