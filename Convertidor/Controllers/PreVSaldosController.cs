using AppService.Api.Utility;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Text;
using Convertidor.Data.Entities;
using Convertidor.Data.Interfaces;
using Convertidor.Services;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using IronPdf;
using Convertidor.Dtos;
using Convertidor.Services.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Ganss.Excel;
using Microsoft.Extensions.Caching.Distributed;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreVSaldosController : ControllerBase
    {
       
        private readonly IPRE_V_SALDOSServices _service;
        private readonly IPRE_V_DENOMINACION_PUCServices _preVSaldosDenominacionPucServices;
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _distributedCache;

        public PreVSaldosController(IPRE_V_SALDOSServices service, 
                                    IPRE_V_DENOMINACION_PUCServices preVSaldosDenominacionPucServices,
                                    IConfiguration configuration,
                                    IDistributedCache distributedCache)
        {

            _service = service;
            _preVSaldosDenominacionPucServices = preVSaldosDenominacionPucServices;
            _configuration = configuration;
            _distributedCache = distributedCache;

        }



       
       
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  GetAll(FilterPRE_V_SALDOSDto filter)
        {
            var result = await _service.GetAll(filter);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAllByPresupuesto(FilterPRE_V_SALDOSDto filter)
        {
            var result = await _service.GetAll(filter);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetPreVDenominacionPuc(FilterPreDenominacionDto filter)
        {
            var result = await _service.GetPreVDenominacionPuc(filter);
            return Ok(result);
        }
        

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAllByPresupuestoIpcPuc(FilterPresupuestoIpcPuc filter)
        {
            ResultDto<List<PreVSaldosGetDto>> result = new ResultDto<List<PreVSaldosGetDto>>(null);
            var cacheKey = $"GetAllByPresupuestoIpcPuc{filter.CodigoPresupuesto.ToString()}-{filter.CodigoPuc.ToString()}-{filter.CodigoIPC.ToString()}";
            var listPresupuesto= await _distributedCache.GetAsync(cacheKey);
            if (listPresupuesto != null)
            {
                result = System.Text.Json.JsonSerializer.Deserialize<ResultDto<List<PreVSaldosGetDto>> > (listPresupuesto);
            }
            else
            {
                result = await _service.GetAllByPresupuestoIpcPuc(filter);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(20))
                    .SetSlidingExpiration(TimeSpan.FromDays(1));
                var serializedList = System.Text.Json.JsonSerializer.Serialize(result);
                var redisListBytes = Encoding.UTF8.GetBytes(serializedList);
                await _distributedCache.SetAsync(cacheKey,redisListBytes,options);
            }
            
            try
            {
                if (result.Data.Count() > 0)
                {

                    ExcelMapper mapper = new ExcelMapper();


                    var settings = _configuration.GetSection("Settings").Get<Settings>();


                    var ruta = @settings.ExcelFiles;  //@"/Users/freveron/Documents/MM/App/full-version/public/ExcelFiles";
                    var fileName = $"SaldoPresupuesto {filter.CodigoPresupuesto.ToString()}-{filter.CodigoIPC.ToString()}-{filter.CodigoPuc.ToString()}.xlsx";
                    string newFile = Path.Combine(Directory.GetCurrentDirectory(), ruta, fileName);


                    mapper.Save(newFile, result.Data, $"SaldoPresupuesto", true);

                    result.LinkData = $"/ExcelFiles/{fileName}";
                }
                else
                {

                    result.IsValid = true;
                    result.Message = "No Data";
                    result.LinkData = $"";
                }
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Message = ex.Message;
                result.LinkData = $"";
            }

            

            return Ok(result);
        }
        
        [HttpPost]  
        [Route("[action]")]
        public async Task<IActionResult> GetAllPRE_V_DENOMINACION_PUC(FilterPRE_V_DENOMINACION_PUC filter)
        {
            var result = await _preVSaldosDenominacionPucServices.GetAll(filter);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetResumenPresupuestoDenominacionPuc(FilterPreVDenominacionPuc filter)
        {
            var result = await _preVSaldosDenominacionPucServices.GetResumenPresupuestoDenominacionPuc(filter);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAllByPresupuestoPucConcat(FilterPresupuestoPucConcat filter)
        {
            var result = await _service.GetAllByPresupuestoPucConcat( filter);
           
            return Ok(result);
        }

    }
}
