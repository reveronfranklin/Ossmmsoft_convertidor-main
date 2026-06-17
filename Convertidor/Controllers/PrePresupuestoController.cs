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
        public async Task<IActionResult> GetAllPresupuestoEntity()
        {
          

            var result = await _prePresupuestoService.GetAllPresupuestoEntity();
            return Ok(result);
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
            try
            {
                if (filter.CodigoPresupuesto == 0)
                {
                    var ultimoPresupuesto = await _prePresupuestoService.GetUltimo();
                    filter.CodigoPresupuesto = ultimoPresupuesto.CODIGO_PRESUPUESTO;
                }

                var cacheKey = $"List<GetPRE_PRESUPUESTOSDto>-{filter.CodigoPresupuesto}-{filter.FinanciadoId}-{filter.FechaDesde:yyyyMMdd}-{filter.FechaHasta:yyyyMMdd}";
                byte[]? listPresupuesto = null;

                try
                {
                    listPresupuesto = await _distributedCache.GetAsync(cacheKey);
                }
                catch
                {
                    listPresupuesto = null;
                }

                if (filter.CodigoPresupuesto > 0 && listPresupuesto != null)
                {
                    var cachedResult = System.Text.Json.JsonSerializer.Deserialize<ResultDto<List<GetPRE_PRESUPUESTOSDto>>>(listPresupuesto);
                    if (cachedResult != null)
                    {
                        return Ok(cachedResult);
                    }
                }

                var result = await _prePresupuestoService.GetAll(filter);

                try
                {
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddDays(20))
                        .SetSlidingExpiration(TimeSpan.FromDays(20));
                    var serializedList = System.Text.Json.JsonSerializer.Serialize(result);
                    var redisListBytes = Encoding.UTF8.GetBytes(serializedList);
                    await _distributedCache.SetAsync(cacheKey, redisListBytes, options);
                }
                catch
                {
                    // El cache no debe impedir que el dashboard responda.
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al consultar presupuesto",
                    detail = ex.Message
                });
            }
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
