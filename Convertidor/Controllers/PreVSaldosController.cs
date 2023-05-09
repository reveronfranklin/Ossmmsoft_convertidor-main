using AppService.Api.Utility;
using System.Drawing.Imaging;
using System.Drawing.Printing;
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

        public PreVSaldosController(IPRE_V_SALDOSServices service, IPRE_V_DENOMINACION_PUCServices preVSaldosDenominacionPucServices, IConfiguration configuration)
        {

            _service = service;
            _preVSaldosDenominacionPucServices = preVSaldosDenominacionPucServices;
            _configuration = configuration;

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
        public async Task<IActionResult> GetAllByPresupuestoIpcPuc(FilterPresupuestoIpcPuc filter)
        {
            var result = await _service.GetAllByPresupuestoIpcPuc(filter);
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
