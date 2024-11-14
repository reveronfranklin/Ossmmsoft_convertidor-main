using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Presupuesto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PreIndiceCategoriaProgramaticaController : ControllerBase
    {
       
        private readonly IIndiceCategoriaProgramaService _indiceCategoriaProgramaService;

        public PreIndiceCategoriaProgramaticaController(IIndiceCategoriaProgramaService indiceCategoriaProgramaService)
        {

            _indiceCategoriaProgramaService = indiceCategoriaProgramaService;
           
        }

        
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListCodigosValidosIcp()
        {
            var result = await _indiceCategoriaProgramaService.ListCodigosValidosIcp();
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListCodigosHistoricoIcp()
        {
            var result = await _indiceCategoriaProgramaService.ListCodigosHistoricoIcp();
           
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  GetAllFilter(FilterByPresupuestoDto filter)
        {
            var res = await _indiceCategoriaProgramaService.UpdateIcpPadre(filter.CodigoPresupuesto);
            var result = await _indiceCategoriaProgramaService.GetAllByCodigoPresupuesto(filter);
            return Ok(result);
            
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetTree(FilterByPresupuestoDto filter)
        {
            //var res = await _indiceCategoriaProgramaService.UpdateIcpPadre(filter.CodigoPresupuesto);
            var result = await _indiceCategoriaProgramaService.GetTreeByPresupuesto(filter.CodigoPresupuesto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(PreIndiceCategoriaProgramaticaUpdateDto dto)
        {
            var result = await _indiceCategoriaProgramaService.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(PreIndiceCategoriaProgramaticaUpdateDto dto)
        {
            var result = await _indiceCategoriaProgramaService.Create(dto);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(DeletePreIcpDto dto)
        {
            var result = await _indiceCategoriaProgramaService.Delete(dto);
            return Ok(result);

        }


    }
}
