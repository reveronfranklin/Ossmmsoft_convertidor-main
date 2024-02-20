
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
    public class BmDetalleBienesController : ControllerBase
    {
       
        private readonly IBmDetalleBienesService _service;

        public BmDetalleBienesController(IBmDetalleBienesService service)
        {

            _service = service;
           
        }
        

        [HttpPost]
        [Route("[action]")]

        public async Task<ActionResult> GetAll() 
        {
            var result = await _service.GetAll();
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(BmDetalleBienesUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(BmDetalleBienesUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(BmDetalleBienesDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }

    }
}
