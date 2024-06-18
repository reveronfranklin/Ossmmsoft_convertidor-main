using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Presupuesto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PrePlanUnicoCuentasController : ControllerBase
    {
       
        private readonly IPrePlanUnicoCuentasService _service;

        public PrePlanUnicoCuentasController(IPrePlanUnicoCuentasService service)
        {

            _service = service;
           
        }

        
      

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListCodigosHistoricoPuc()
        {
            var result = await _service.ListCodigosHistoricoPuc();
           
            return Ok(result);
        }   

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  GetAllFilter(FilterByPresupuestoDto filter)
        {
            var res = await _service.UpdatePucPadre(filter.CodigoPresupuesto);
            var result = await _service.GetAllByCodigoPresupuesto(filter.CodigoPresupuesto);
                return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetTree(FilterByPresupuestoDto filter)
        {
            
            var result = await _service.GetTreeByPresupuesto(filter.CodigoPresupuesto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(PrePlanUnicoCuentaUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(PrePlanUnicoCuentaUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(DeletePrePucDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }


    }
}
