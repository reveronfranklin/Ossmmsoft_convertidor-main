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
    public class RhTitularesBeneficiariosController : ControllerBase
    {
       
        private readonly IRhVTitularesBeneficiariosService _service;

        public RhTitularesBeneficiariosController(IRhVTitularesBeneficiariosService service)
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
        public async Task<IActionResult> GetByTipoNomina(FilterTipoNomina filter)
        {
            var result = await _service.GetByTipoNomina(filter);
            return Ok(result);
        }

      

    }
}
