
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
    
    public async Task<string> GeneratePdf(FilterResumenSaldo filter)
    {
        var settings = _configuration.GetSection("Settings").Get<Settings>();
        var result = "No Data";
        var pathLogo = @settings.BmFiles + "LogoIzquierda.jpeg";
        var fileName= $"ResumenSaldo-{filter.CodigoPresupuesto}.pdf";
        var filePath = $"{ @settings.ExcelFiles}{@settings.SeparatorPatch}{fileName}.pdf";
        FilterPRE_PRESUPUESTOSDto filterPresupuesto = new FilterPRE_PRESUPUESTOSDto();
        filterPresupuesto.CodigoPresupuesto = filter.CodigoPresupuesto;
        var presupuesto = await _presupuestosService.GetByCodigo(filterPresupuesto);
        var resumen = await _services.GetAllByPresupuesto(filter.CodigoPresupuesto);
        if (resumen.IsValid == false)
        {
            return "NO_DATA.pdf";
        }
        else
        {
            var document = new ResumenSaldoDocument(presupuesto.Data.Denominacion,resumen.Data,pathLogo);
            fileName= $"ResumenSaldo-{filter.CodigoPresupuesto}.pdf";
            filePath = $"{ @settings.ExcelFiles}{@settings.SeparatorPatch}{fileName}";
            document.GeneratePdf(filePath);
            result =fileName;
          
        }
      
        return result;
    }
    
    
}