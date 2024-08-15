using System.Text;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Presupuesto;
using Microsoft.Extensions.Caching.Distributed;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    //[Authorize]
    public class PrePresupuestoController : ControllerBase
    {
       
        private readonly IPRE_PRESUPUESTOSService _prePresupuestoService;
        private readonly IDistributedCache _distributedCache;
        public PrePresupuestoController(IPRE_PRESUPUESTOSService prePresupuestoService,
                                        IDistributedCache distributedCache)
        {

            _prePresupuestoService = prePresupuestoService;
            _distributedCache = distributedCache;
        }


        [HttpGet]
        [Route("[action]")]
        
        public async Task<IActionResult> GetAll()
        {
            FilterPRE_PRESUPUESTOSDto filter = new FilterPRE_PRESUPUESTOSDto();
            filter.CodigoEmpresa = 13;
            filter.CodigoEmpresa = 0;
            filter.SearchText = ""; 

            var result = await _prePresupuestoService.GetAll(filter);
            return Ok(result.Data);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetList()
        {
            FilterPRE_PRESUPUESTOSDto filter = new FilterPRE_PRESUPUESTOSDto();
            filter.CodigoEmpresa = 13;
            filter.CodigoEmpresa = 0;
            filter.SearchText = "";

            var result = await _prePresupuestoService.GetList(filter);
            return Ok(result.Data);
        }



        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetListPresupuesto()
        {
          
            var result = await _prePresupuestoService.GetListPresupuesto();
            return Ok(result.Data);
        }
                

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  GetAllFilter(FilterPRE_PRESUPUESTOSDto filter)
        {
            if (filter.CodigoPresupuesto == 0)
            {
                var ultimoPresupuesto = await _prePresupuestoService.GetUltimo();
                filter.CodigoPresupuesto = ultimoPresupuesto.CODIGO_PRESUPUESTO;
            }
            
                ResultDto<List<GetPRE_PRESUPUESTOSDto>> result = new ResultDto<List<GetPRE_PRESUPUESTOSDto>>(null);
                var cacheKey = $"List<GetPRE_PRESUPUESTOSDto>-{filter.CodigoPresupuesto.ToString()}-{filter.FinanciadoId.ToString()}-{filter.FechaDesde.ToShortDateString()}-{filter.FechaHasta.ToShortDateString()}";
                var listPresupuesto= await _distributedCache.GetAsync(cacheKey);
                if (filter.CodigoPresupuesto> 0 &&  listPresupuesto!= null)
                {
                    result = System.Text.Json.JsonSerializer.Deserialize<ResultDto<List<GetPRE_PRESUPUESTOSDto>> > (listPresupuesto);
                }
                else
                {
                    result = await _prePresupuestoService.GetAll(filter);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddDays(1))
                        .SetSlidingExpiration(TimeSpan.FromDays(1));
                    var serializedList = System.Text.Json.JsonSerializer.Serialize(result);
                    var redisListBytes = Encoding.UTF8.GetBytes(serializedList);
                    await _distributedCache.SetAsync(cacheKey,redisListBytes,options);
                }
               
                return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByCode(FilterPRE_PRESUPUESTOSDto filter)
        {
            var result = await _prePresupuestoService.GetByCodigo(filter);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(CreatePRE_PRESUPUESTOSDto dto)
        {
            var result = await _prePresupuestoService.Create(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(UpdatePRE_PRESUPUESTOSDto dto)
        {
            var result = await _prePresupuestoService.Update(dto);
            return Ok(result);
        }

        [HttpPost] 
        [Route("[action]")]
        public async Task<IActionResult> AprobarPresupuesto(AprobaPrePresupuestoDto dto)
        {
            var result = await _prePresupuestoService.AprobarPresupuesto(dto.CodigoPresupuesto,dto.FechaAprobacion);
            return Ok(result);
        }
        

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(DeletePrePresupuestoDto dto)
        {
            var result = await _prePresupuestoService.Delete(dto);
            return Ok(result);
        }


       
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HistoricoNominaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
