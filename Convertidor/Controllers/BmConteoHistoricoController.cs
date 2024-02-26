
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Convertidor.Services.Bm;
using Convertidor.Dtos.Bm;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BmConteoHistoricoController : ControllerBase
    {
       
        private readonly IBmConteoHistoricoService _service;

        public BmConteoHistoricoController(IBmConteoHistoricoService service)
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





    }
}
