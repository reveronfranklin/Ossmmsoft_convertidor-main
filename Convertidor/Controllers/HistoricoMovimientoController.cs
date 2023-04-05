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
using Convertidor.Services.Rh;
using Convertidor.Dtos.Rh;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoMovimientoController : ControllerBase
    {
       
        private readonly IRhHistoricoMovimientoService _historicoNominaService;
    
        public HistoricoMovimientoController(IRhHistoricoMovimientoService historicoNominaService
                                         )
        {
          
            _historicoNominaService = historicoNominaService;
        
        }

  


        // POST api/<HistoricoNominaController>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  GetHistoricoPeriodoTipoNomina(FilterHistoricoNominaPeriodo filter)
        {
            List<ListHistoricoMovimientoDto> result = new List<ListHistoricoMovimientoDto>();

 
                 result = await _historicoNominaService.GetByTipoNominaPeriodo(filter.TipoNomina, filter.CodigoPeriodo);
    
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetHistoricoFecha(FilterHistoricoNominaPeriodo filter)
        {
            List<ListHistoricoMovimientoDto> result = new List<ListHistoricoMovimientoDto>();


                result = await _historicoNominaService.GetByFechaNomina(filter.Desde, filter.Hasta);
                return Ok(result);
          
            
            return Ok(result);
        }


        /*[HttpPost]
        public async Task<IActionResult> GetFechaNomina(FilterHistoricoNominaPeriodo filter)
        {
            var result = await _historicoNominaService.GetByFechaNomina(filter.Desde, filter.Hasta);
            return Ok(result);
        }*/

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
