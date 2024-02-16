
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
    public class BmDetalleArticulosController : ControllerBase
    {
       
        private readonly IBmDetalleArticulosService _service;

        public BmDetalleArticulosController(IBmDetalleArticulosService service)
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
        public async Task<IActionResult> Create(BmDetalleArticulosUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(BmDetalleArticulosUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(BmDetalleArticulosDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }

    }
}
