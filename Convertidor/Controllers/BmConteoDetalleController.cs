
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
    public class BmConteoDetalleController : ControllerBase
    {
       
        private readonly IBmConteoDetalleService _service;

        public BmConteoDetalleController(IBmConteoDetalleService service)
        {

            _service = service;
           
        }
        

        [HttpPost]
        [Route("[action]")]

        public async Task<ActionResult> GetAllByConteo(BmConteoFilterDto filter) 
        {
            var result = await _service.GetAllByConteo(filter);
            return Ok(result);
        }
        


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(BmConteoDetalleUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }
      


    }
}
