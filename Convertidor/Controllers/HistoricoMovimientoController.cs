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
using Microsoft.AspNetCore.StaticFiles;

using Ganss.Excel;
using Convertidor.Utility;
using NPOI.HPSF;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoMovimientoController : ControllerBase
    {
       
        private readonly IRhHistoricoMovimientoService _historicoNominaService;
        private readonly IConfiguration _configuration;
        public HistoricoMovimientoController(IRhHistoricoMovimientoService historicoNominaService,
                                             IConfiguration configuration
                                         )
        {
          
            _historicoNominaService = historicoNominaService;
            _configuration = configuration;
        }

  


        // POST api/<HistoricoNominaController>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  GetHistoricoPeriodoTipoNomina(FilterHistoricoNominaPeriodo filter)
        {
            List<ListHistoricoMovimientoDto> result = new List<ListHistoricoMovimientoDto>();

         
            result = await _historicoNominaService.GetByTipoNominaPeriodo(filter.CodigoTipoNomina, 1);
    
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetHistoricoFecha(FilterHistoricoNominaPeriodo filter)
        {
            ResultDto<List<ListHistoricoMovimientoDto>> resultDto = new ResultDto<List<ListHistoricoMovimientoDto>>(null);
            List<ListHistoricoMovimientoDto> result = new List<ListHistoricoMovimientoDto>();


            if (!DateValidate.IsDate(filter.Desde.ToShortDateString()))
            {
                resultDto.Data = null;
                resultDto.IsValid = false;
                resultDto.Message = "Fecha Desde Invalida";
                resultDto.LinkData = "";
                return Ok(resultDto);
            }
            if (!DateValidate.IsDate(filter.Hasta.ToShortDateString()))
            {
                resultDto.Data = null;
                resultDto.IsValid = false;
                resultDto.Message = "Fecha Hasta Invalida";
                resultDto.LinkData = "";
                return Ok(resultDto);
            }


            result = await _historicoNominaService.GetByFechaNomina(filter.Desde, filter.Hasta);

            if (filter.CodigoPersona > 0)
            {
                result = result.Where(x => x.CodigoPersona == filter.CodigoPersona).ToList();
               

                if (!string.IsNullOrEmpty(filter.CodigoConcepto))
                {
                    var concepto = result.Where(x => x.Codigo == filter.CodigoConcepto).ToList();
                    if (concepto.Count > 0) { result = concepto; }
                }
            }
            else
            {
                if (filter.CodigoTipoNomina > 0)
                {
                    var tipoNomina = result.Where(x => x.CodigoTipoNomina == filter.CodigoTipoNomina).ToList();

                    result = tipoNomina; 
                }

                if (!string.IsNullOrEmpty(filter.CodigoConcepto))
                {
                    var concepto = result.Where(x => x.Codigo == filter.CodigoConcepto).ToList();
                    result = concepto; 
                }
            }

         


            if (result.Count() > 0)
            {

                ExcelMapper mapper = new ExcelMapper();


                var settings = _configuration.GetSection("Settings").Get<Settings>();


                var ruta = @settings.ExcelFiles;  //@"/Users/freveron/Documents/MM/App/full-version/public/ExcelFiles";
                var fileName = $"HistoricoNominaDesde {filter.Desde.Year.ToString()}-{filter.Desde.Month.ToString()}-{filter.Desde.Day.ToString()} Hasta {filter.Hasta.Year.ToString()}-{filter.Hasta.Month.ToString()}-{filter.Hasta.Day.ToString()}.xlsx";
                string newFile = Path.Combine(Directory.GetCurrentDirectory(), ruta, fileName);


                mapper.Save(newFile, result, $"HistoricoNomina", true);
                resultDto.Data = result;
                resultDto.IsValid = true;
                resultDto.Message = "";
                resultDto.LinkData = $"/ExcelFiles/{fileName}";
                //if (filter.Page == 0) filter.Page = 1;
               // var historico = Paginacion<ListHistoricoMovimientoDto>.CrearPaginacion(result, filter.Page, filter.PageSize);
                //resultDto.Data = historico;
                resultDto.Page = filter.Page;
                resultDto.CantidadRegistros = result.Count();
            }
            else
            {
                resultDto.Data = result;
                resultDto.IsValid = true;
                resultDto.Message = "No Data";
                resultDto.LinkData = $"";
            }


            
         

            return Ok(resultDto);
          
          
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GenerateExcel(FilterHistoricoNominaPeriodo filter)
        {
            List<ListHistoricoMovimientoDto> historico = new List<ListHistoricoMovimientoDto>();


            historico = await _historicoNominaService.GetByFechaNomina(filter.Desde, filter.Hasta);

            ExcelMapper mapper = new ExcelMapper();
            var ruta = @"/Users/freveron/Documents/MM/App/full-version/public";

            string newFile = Path.Combine(Directory.GetCurrentDirectory(), ruta, filter.Desde.Ticks.ToString() + ".xlsx");


            mapper.Save(newFile, historico, "SheetName", true);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(newFile, out var contettype))
            {
                contettype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(newFile);
            var result = File(bytes, contettype, Path.Combine(newFile));
            return result;

            /*string emailtemplatepath = Path.Combine(Directory.GetCurrentDirectory(), "ExcelTemplate//ProductReport.html");
            string htmldata = System.IO.File.ReadAllText(emailtemplatepath);

            string excelstring = "";
            foreach (ListHistoricoMovimientoDto prod in historico)
            {
                excelstring += "<tr><td>" + prod.Nombre + "</td><td>" + prod.Apellido + "</td><td>" + prod.Sueldo + "</td></tr>";
            }
            htmldata = htmldata.Replace("@@ActualData", excelstring);

            string StoredFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ExcelFiles", DateTime.Now.Ticks.ToString() + ".xls");
            System.IO.File.AppendAllText(StoredFilePath, htmldata);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(StoredFilePath, out var contettype))
            {
                contettype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(StoredFilePath);

            return File(bytes, contettype, Path.Combine(StoredFilePath));*/




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
