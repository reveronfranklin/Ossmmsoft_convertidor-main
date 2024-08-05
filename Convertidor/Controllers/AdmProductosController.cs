using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Adm;
using Convertidor.Services.Adm;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdmProductosController : ControllerBase
    {
       
        private readonly IAdmProductosService _service;

        public AdmProductosController(IAdmProductosService service)
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

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAllPaginate(AdmProductosFilterDto filter)
        {
            var result = await _service.GetAllPaginate(filter);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  Update(AdmProductosUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        
       




    }
}
