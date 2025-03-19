using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Sis;
using Convertidor.Services.Sis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SisDescriptivasController : ControllerBase
    {
       
        private readonly ISisDescriptivaService _service;

        public SisDescriptivasController(ISisDescriptivaService service)
        {

            _service = service;


        }
        
        
   
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAllByTitulo(SisDescriptivaFilterDto dto)
        {
            var result = await _service.GetAllByTitulo(dto);
            return Ok(result);
        }
       
       
    
    }
}
