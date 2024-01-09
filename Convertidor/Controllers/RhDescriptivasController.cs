using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhDescriptivasController : ControllerBase
    {
       
        private readonly IRhDescriptivasService _service;

        public RhDescriptivasController(IRhDescriptivasService service)
        {

            _service = service;


        }


   
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByTitulo(FilterRhDescriptiva dto)
        {
            var result = await _service.GetByTitulo(dto.TituloId);
            return Ok(result);
        }
 


    }
}
