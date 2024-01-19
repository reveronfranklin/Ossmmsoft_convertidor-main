
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Rh;
using Convertidor.Dtos.Rh;
using Microsoft.AspNetCore.Authorization;
using Convertidor.Services.Bm;
using Convertidor.Dtos.Bm;
using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BmSolMovBienesController : ControllerBase
    {
       
        private readonly IBmSolMovBienesService _service;

        public BmSolMovBienesController(IBmSolMovBienesService service)
        {

            _service = service;
           
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(BmSolMovBienesUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(BmSolMovBienesUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(BmSolMovBienesDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }

    }
}
