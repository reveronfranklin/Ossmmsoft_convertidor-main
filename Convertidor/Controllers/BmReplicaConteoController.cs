using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Adm;
using Convertidor.Services.Adm;
using Convertidor.Services.Bm.Replica;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BmReplicaConteoController : ControllerBase
    {
       
        private readonly IBmReplicaConteoService _service;

        public BmReplicaConteoController(IBmReplicaConteoService service)
        {

            _service = service;


        }



        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetByLote()
        {
            var result = await _service.ReplicarConteo();
            return Ok(result);
        }

   


    }
}
