using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Services.Presupuesto;
using Convertidor.Dtos.Presupuesto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PreMtrUnidadEjecutoraController : ControllerBase
    {
       
        private readonly IPRE_V_MTR_UNIDAD_EJECUTORAService _service;

        public PreMtrUnidadEjecutoraController(IPRE_V_MTR_UNIDAD_EJECUTORAService service)
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
        public async Task<IActionResult> GetByPresupuesto(FilterByPresupuestoDto filter)
        {
            var result = await _service.GetAllByPresupuesto(filter.CodigoPresupuesto);
            return Ok(result);
        }

       


    }
}
