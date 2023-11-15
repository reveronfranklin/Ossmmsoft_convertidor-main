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
using Convertidor.Data.Repository.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Entities.Rh;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoMovimientoController : ControllerBase
    {
       
        private readonly IRhHistoricoMovimientoService _historicoNominaService;
        private readonly IRhTipoNominaService    _tipoNominaService;
        private readonly IRhProcesoDetalleRepository _rhProcesoDetalleRepository;
        private readonly IRhConceptosRepository _rhConceptosRepository;

        private readonly IConfiguration _configuration;
        public HistoricoMovimientoController(IRhHistoricoMovimientoService historicoNominaService,
                                             IRhTipoNominaService tipoNominaService,
                                             IConfiguration configuration,
                                             IRhProcesoDetalleRepository rhProcesoDetalleRepository,
                                             IRhConceptosRepository rhConceptosRepository
                                         )
        {
          
            _historicoNominaService = historicoNominaService;
            _configuration = configuration;
            _tipoNominaService = tipoNominaService;
            _rhProcesoDetalleRepository = rhProcesoDetalleRepository;
            _rhConceptosRepository = rhConceptosRepository;
        }

  


        // POST api/<HistoricoNominaController>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  GetHistoricoPeriodoTipoNomina(FilterHistoricoNominaPeriodo filter)
        {
            List<ListHistoricoMovimientoDto> result = new List<ListHistoricoMovimientoDto>();

         
            result = await _historicoNominaService.GetByTipoNominaPeriodo(filter.CodigoTipoNomina.FirstOrDefault().CodigoTipoNomina, 1);
    
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetHistoricoFecha(FilterHistoricoNominaPeriodo filter)
        {
            //TipoQuery=> "INDIVIDUAL", "MASIVO", "PROCESO"


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

            switch (filter.TipoQuery)
            {
                case "INDIVIDUAL":
                    result = await _historicoNominaService.GetByIndividual(filter);
                    break;
                case "MASIVO":
                    result = await _historicoNominaService.GetByMasivo(filter);
                    break;
                case "PROCESO":
                    result = await _historicoNominaService.GetByProceso(filter);           
                    break;
                default:
                    // code block
                    break;
            }

           


            result = result.OrderBy(x => x.FechaNomina).ToList();

            if (result.Count() > 0)
            {

                List<ListHistoricoMovimientoExcelDto> excelData = new List<ListHistoricoMovimientoExcelDto>();
                excelData = await MapListHistoricoMovimiento(result);
                ExcelMapper mapper = new ExcelMapper();


                var settings = _configuration.GetSection("Settings").Get<Settings>();

               
                var ruta = @settings.ExcelFiles;  //@"/Users/freveron/Documents/MM/App/full-version/public/ExcelFiles";
                var fileName = $"HistoricoNominaDesde {filter.Desde.Year.ToString()}-{filter.Desde.Month.ToString()}-{filter.Desde.Day.ToString()} Hasta {filter.Hasta.Year.ToString()}-{filter.Hasta.Month.ToString()}-{filter.Hasta.Day.ToString()}.xlsx";
                string newFile = Path.Combine(Directory.GetCurrentDirectory(), ruta, fileName);


                mapper.Save(newFile, excelData, $"HistoricoNomina", true);
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
        public async Task<IActionResult> GetResumenPago( FilterPersonaDto filter)
        {
            ResultDto<List<RhResumenPagoPorPersona>> resultDto = new ResultDto<List<RhResumenPagoPorPersona>>(null);
         
            List<RhResumenPagoPorPersona> result = new List<RhResumenPagoPorPersona>();

            result = await _historicoNominaService.GetResumenPagoCodigoPersona(filter.CodigoPersona);
            

            result = result.OrderByDescending(x => x.FechaNomina).ToList();

            if (result.Count() > 0)
            {
                resultDto.Data = result;
                resultDto.IsValid = true;
                resultDto.Message = "";
                resultDto.LinkData = $"";
                //if (filter.Page == 0) filter.Page = 1;
               // var historico = Paginacion<ListHistoricoMovimientoDto>.CrearPaginacion(result, filter.Page, filter.PageSize);
                //resultDto.Data = historico;
                resultDto.Page = 0;
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

        private async Task<List<ListHistoricoMovimientoExcelDto>> MapListHistoricoMovimiento(List<ListHistoricoMovimientoDto> dto)
        {

            List<ListHistoricoMovimientoExcelDto> result = new List<ListHistoricoMovimientoExcelDto>();
           
            try
            {
                var resultNew = dto

                 .Select(e => new ListHistoricoMovimientoExcelDto
                 {
                  
                   
                     Cedula = e.Cedula,
                     Nombre = e.Nombre,
                     Apellido = e.Apellido,
                     Nacionalidad = e.Nacionalidad,
                     DescripcionNacionalidad = e.DescripcionNacionalidad,
                     Sexo = e.Sexo,
                     Status = e.Status,
                     DescripcionStatus = e.DescripcionStatus,
                     Sueldo = e.Sueldo,
                     DescripcionCargo = e.DescripcionCargo,
                   
                     TipoNomina = e.TipoNomina,
                   
                     DescripcionTipoCuenta = e.DescripcionTipoCuenta,
                   
                     DescripcionBanco = e.DescripcionBanco,
                     NoCuenta = e.NoCuenta,
                     FechaNominaMov = e.FechaNominaMov,
                     FechaNomina = e.FechaNomina,
                     Complemento = e.Complemento,
                     Tipo = e.Tipo,
                     Monto = e.Monto,
                     StatusMov = e.StatusMov,
                     Codigo = e.Codigo,
                     Denominacion = e.Denominacion,
      
                     UnidadEjecutora = e.UnidadEjecutora,
                     EstadoCivil = e.EstadoCivil

                 });
                result = resultNew.ToList();

                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }







        }

    }
}
