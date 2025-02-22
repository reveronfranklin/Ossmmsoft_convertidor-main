using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Services.Bm;
using Convertidor.Dtos.Bm;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BmUbicacionesController : ControllerBase
    {
       
        private readonly IBmUbicacionesService _service;

        public BmUbicacionesController(IBmUbicacionesService service)
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






    }
}
