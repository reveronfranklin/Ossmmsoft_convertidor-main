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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoNominaController : ControllerBase
    {
       
        private readonly IHistoricoNominaService _historicoNominaService;
        private readonly IHistoricoPersonalCargoService _historicoPersonalCargoService;
        private readonly IIndiceCategoriaProgramaService _indiceCategoriaProgramaService;
        private readonly IConceptosRetencionesService _conceptosRetencionesService;
        private readonly IHistoricoRetencionesService _historicoRetencionesService;
        private readonly IPetroClientService _petroClient;
        public HistoricoNominaController(IHistoricoNominaService historicoNominaService,
                                         IHistoricoPersonalCargoService historicoPersonalCargoService,
                                         IIndiceCategoriaProgramaService indiceCategoriaProgramaService,
                                         IConceptosRetencionesService conceptosRetencionesService,
                                         IHistoricoRetencionesService historicoRetencionesService,
                                         IPetroClientService petroClient)
        {
          
            _historicoNominaService = historicoNominaService;
            _historicoPersonalCargoService = historicoPersonalCargoService;
            _indiceCategoriaProgramaService = indiceCategoriaProgramaService;
            _conceptosRetencionesService = conceptosRetencionesService;
            _historicoRetencionesService = historicoRetencionesService;
            _petroClient = petroClient;
        }

      

        // GET api/<HistoricoNominaController>/5
        [HttpGet("{dias}")]
        public async Task<IActionResult> Get(int dias)
        {
            // var  resultConceptosRetenciones = await _conceptosRetencionesService.CrearConceptosRetencionBase();
            // var  resultIndiceCategoriaprograma = await _indiceCategoriaProgramaService.TransferirIndiceCategoriaProgramaPorCantidadDeDias(dias);

            // var resultPersonalCargo = await _historicoPersonalCargoService.TransferirHistoricoPersonalCargoPorCantidadDeDias(dias);

            var resultPetro =await _petroClient.GetPetroFiat();

            //var Renderer = new IronPdf.ChromePdfRenderer();

           // Renderer.RenderHtmlAsPdf("<h1>Html with CSS and Images</h1>").SaveAs("pixel-perfect.pdf");



            /****** Advanced ******/

            // Load external html assets: images, css and javascript.

            // An optional BasePath 'C:\site\assets\' is set as the file location to load assets from

            //var PDF = Renderer.RenderHtmlAsPdf("<img src='icons/iron.png'>", @"C:\site\assets\");

            //PDF.SaveAs("html-with-assets.pdf");


            var result = await _historicoNominaService.TransferirHistoricoNominaPorCantidadDeDias(dias);
            var resulHistoricoRetenciones = await _historicoRetencionesService.GeneraHistoricoRetencionesPorCantidadDeDias(dias);

            return Ok(resulHistoricoRetenciones);
        }


       



        // POST api/<HistoricoNominaController>
        [HttpPost]
        public async Task<IActionResult>  GetHistoricoPeriodoTipoNomina(FilterHistoricoNominaPeriodo filter)
        {
            var result = await _historicoNominaService.GetByPeriodo(1, filter.CodigoTipoNomina);
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
