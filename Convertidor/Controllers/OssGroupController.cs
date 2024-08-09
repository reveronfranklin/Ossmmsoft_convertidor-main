
using Microsoft.AspNetCore.Mvc;
using Convertidor.Dtos.Sis;
using Convertidor.Services.Sis;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OssAuthGroupController : ControllerBase
    {
        private readonly IOssAuthGroupService _service;


        public OssAuthGroupController(IOssAuthGroupService service)
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
        public async Task<IActionResult> GetById(AuthGroupFilterDto filter)
        {
            var result = await _service.GetById(filter);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AuthGroupUpdateDto filter)
        {
            var result = await _service.Update(filter);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AuthGroupUpdateDto filter)
        {
            var result = await _service.Create(filter);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AuthGroupDeleteDto filter)
        {
            var result = await _service.Delete(filter);
            return Ok(result);
        }
      
    }
}
