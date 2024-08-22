
using Microsoft.AspNetCore.Mvc;
using Convertidor.Dtos.Sis;
using Convertidor.Services.Sis;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OssAuthUserGroupController : ControllerBase
    {
        private readonly IOssAuthUserGroupService _service;


        public OssAuthUserGroupController(IOssAuthUserGroupService service)
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
        public async Task<IActionResult> GetById(AuthUserGroupFilterDto filter)
        {
            var result = await _service.GetById(filter);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByUser(AuthUserGroupFilterDto filter)
        {
            var result = await _service.GetByUser(filter);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AuthUserGroupUpdateDto filter)
        {
            var result = await _service.Create(filter);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AuthUserGroupDeleteDto filter)
        {
            var result = await _service.Delete(filter);
            return Ok(result);
        }
      
    }
}
