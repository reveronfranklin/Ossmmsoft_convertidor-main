
using Microsoft.AspNetCore.Mvc;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PreAsignacionesController : ControllerBase
    {
       
        private readonly IPreAsignacionService _service;
        private readonly IPRE_PRESUPUESTOSService _presupuestosService;

        public PreAsignacionesController(IPreAsignacionService service,IPRE_PRESUPUESTOSService presupuestosService)
        {
            _service = service;
            _presupuestosService = presupuestosService;
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> GetAll(PreAsignacionesFilterDto filter)
        {
            
            ResultDto<List<PreAsignacionesGetDto>> result = new ResultDto<List<PreAsignacionesGetDto>>(null);
            if (filter.CodigoPresupuesto == null || filter.CodigoPresupuesto == 0)
            {
                var presipuesto = await _presupuestosService.GetUltimo();
                filter.CodigoPresupuesto = presipuesto.CODIGO_PRESUPUESTO;
            }
         
            if (filter.CodigoIcp == 0 && filter.CodigoPuc == 0)
            {
                result = await _service.GetAllByPresupuesto(filter);
            }
            if (filter.CodigoIcp > 0 && filter.CodigoPuc == 0)
            {
                result = await _service.GetAllByIcp(filter);
            }
            if (filter.CodigoIcp > 0 && filter.CodigoPuc > 0)
            {
                result = await _service.GetAllByIcpPuc(filter);
            }
          
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> GetAllByPresupuesto(PreAsignacionesFilterDto filter)
        {
            if (filter.CodigoPresupuesto == null || filter.CodigoPresupuesto == 0)
            {
                var presipuesto = await _presupuestosService.GetUltimo();
                filter.CodigoPresupuesto = presipuesto.CODIGO_PRESUPUESTO;
            }

          
            var result = await _service.GetAllByPresupuesto(filter);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> GetAllByIcp(PreAsignacionesFilterDto filter)
        {
            if (filter.CodigoPresupuesto == null || filter.CodigoPresupuesto == 0)
            {
                var presipuesto = await _presupuestosService.GetUltimo();
                filter.CodigoPresupuesto = presipuesto.CODIGO_PRESUPUESTO;
            }
            var result = await _service.GetAllByIcp(filter);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult>  GetAllByIcpPuc(PreAsignacionesFilterDto filter)
        {
            if (filter.CodigoPresupuesto == null || filter.CodigoPresupuesto == 0)
            {
                var presipuesto = await _presupuestosService.GetUltimo();
                filter.CodigoPresupuesto = presipuesto.CODIGO_PRESUPUESTO;
            }
            var result = await _service.GetAllByIcpPuc(filter);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateListAsignaciones(PreAsignacionesExcel dto)
        {
            var result = await _service.CreateListAsignaciones(dto);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ValidarListAsignaciones(PreAsignacionesExcel dto)
        {
            var result = await _service.ValidarListAsignaciones(dto);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(PreAsignacionesUpdateDto dto)
        {
            var result = await _service.Add(dto);
            
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(PreAsignacionesUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateField(UpdateFieldDto dto)
        {
            var result = await _service.UpdateField(dto);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(PreAsignacionesDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }

    }
}
