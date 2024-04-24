
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Bm;
using Convertidor.Dtos.Bm;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BmConteoController : ControllerBase
    {
       
        private readonly IBmConteoService _service;

        public BmConteoController(IBmConteoService service)
        {

            _service = service;
           
        }
        

        [HttpGet]
        [Route("[action]")]

        public async Task<ActionResult> GetAll() 
        {
            var result = await _service.GetAll();
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(BmConteoUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(BmConteoUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CerrarConteo(BmConteoCerrarDto dto)
        {
            var result = await _service.CerrarConteo(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(BmConteoDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateReportConteo(BmConteoFilterDto dto)
        {
            await _service.CreateReportConteo(dto.CodigoBmConteo);
            return Ok();
        }

    }
}
