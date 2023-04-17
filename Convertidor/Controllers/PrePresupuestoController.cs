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
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PrePresupuestoController : ControllerBase
    {
       
        private readonly IPRE_PRESUPUESTOSService _prePresupuestoService;

        public PrePresupuestoController(IPRE_PRESUPUESTOSService prePresupuestoService)
        {

            _prePresupuestoService = prePresupuestoService;
           
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
        public async Task<IActionResult> GetListPresupuesto()
        {
          

            var result = await _prePresupuestoService.GetListPresupuesto();
            return Ok(result.Data);


        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  GetAllFilter(FilterPRE_PRESUPUESTOSDto filter)
        {
            var result = await _prePresupuestoService.GetAll(filter);
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

        // PUT api/<HistoricoNominaController>/5
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
