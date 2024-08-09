
using Microsoft.AspNetCore.Mvc;
using Convertidor.Dtos.Sis;
using Convertidor.Services.Sis;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OssAuthPermissionController : ControllerBase
    {
        private readonly IOssAuthPermissionsService _service;


        public OssAuthPermissionController(IOssAuthPermissionsService service)
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
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetListCodeName()
        {
            var result = _service.GetListCodeName();
            return Ok(result);
        }
        
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetById(AuthPermissionFilterDto filter)
        {
            var result = await _service.GetById(filter);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AuthPermissionUpdateDto filter)
        {
            var result = await _service.Update(filter);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AuthPermissionUpdateDto filter)
        {
            var result = await _service.Create(filter);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AuthPermissionDeleteDto filter)
        {
            var result = await _service.Delete(filter);
            return Ok(result);
        }
      
    }
}
