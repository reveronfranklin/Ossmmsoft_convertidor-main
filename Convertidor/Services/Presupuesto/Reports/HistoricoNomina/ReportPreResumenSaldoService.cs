
using Convertidor.Dtos.Presupuesto;
using QuestPDF.Fluent;

namespace Convertidor.Services.Rh.Report.Example;

public class ReportPreResumenSaldoService:IReportPreResumenSaldoService
{
    private readonly IConfiguration _configuration;
    private readonly IPreResumenSaldoServices _services;
    private readonly IPRE_PRESUPUESTOSService _presupuestosService;


    public ReportPreResumenSaldoService(IConfiguration configuration,
                                        IPreResumenSaldoServices services,
                                        IPRE_PRESUPUESTOSService presupuestosService)
    {
        _configuration = configuration;
        _services = services;
        _presupuestosService = presupuestosService;
    }
    
    public async Task<ResultDto<string>> GeneratePdf(FilterResumenSaldo filter)
    {
        ResultDto<string> result = new ResultDto<string>("");

        var settings = _configuration.GetSection("Settings").Get<Settings>();
        result.Data = "No Data";
        var pathLogo = @settings.BmFiles + "LogoIzquierda.jpeg";
        var fileName= $"ResumenSaldo-{filter.CodigoPresupuesto}.pdf";
        var filePath = $"{ @settings.ExcelFiles}{@settings.SeparatorPatch}{fileName}.pdf";
        FilterPRE_PRESUPUESTOSDto filterPresupuesto = new FilterPRE_PRESUPUESTOSDto();
        filterPresupuesto.CodigoPresupuesto = filter.CodigoPresupuesto;
        var presupuesto = await _presupuestosService.GetByCodigo(filterPresupuesto);
        var resumen = await _services.GetAllByPresupuesto(filter.CodigoPresupuesto);
        if (resumen.IsValid == false)
        {
            result.Data = "No Data";
            result.IsValid = true;
            result.Message = "No existen Datos";
        }
        else
        {
            var document = new ResumenSaldoDocument(presupuesto.Data.Denominacion,resumen.Data,pathLogo);
            fileName= $"ResumenSaldo-{filter.CodigoPresupuesto}.pdf";
            filePath = $"{ @settings.ExcelFiles}{@settings.SeparatorPatch}{fileName}";
            document.GeneratePdf(filePath);
            result.Data =fileName;
            result.IsValid = true;
            result.Message = "";
          
        }
      
        return result;
    }
    
    
}