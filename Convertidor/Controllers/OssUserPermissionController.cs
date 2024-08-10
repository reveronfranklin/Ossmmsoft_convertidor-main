
using Microsoft.AspNetCore.Mvc;
using Convertidor.Dtos.Sis;
using Convertidor.Services.Sis;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OssAuthUserPermissionController : ControllerBase
    {
        private readonly IOssAuthUserPermissionService _service;


        public OssAuthUserPermissionController(IOssAuthUserPermissionService service)
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
        public async Task<IActionResult> GetById(AuthUserPermisionFilterDto filter)
        {
            var result = await _service.GetById(filter);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByUser(AuthUserPermisionFilterDto filter)
        {
            var result = await _service.GetByUser(filter);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AuthUserPermisionUpdateDto filter)
        {
            var result = await _service.Create(filter);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AuthUserPermisionDeleteDto filter)
        {
            var result = await _service.Delete(filter);
            return Ok(result);
        }
      
    }
}
