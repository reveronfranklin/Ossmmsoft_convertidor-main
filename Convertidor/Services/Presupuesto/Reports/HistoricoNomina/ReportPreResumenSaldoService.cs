
using Convertidor.Dtos.Presupuesto;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
namespace Convertidor.Services.Rh.Report.Example;

public class ReportPreResumenSaldoService:IReportPreResumenSaldoService
{
    private readonly IConfiguration _configuration;
    private readonly IPreResumenSaldoServices _services;
  

    public ReportPreResumenSaldoService(IConfiguration configuration,
                                        IPreResumenSaldoServices services)
    {
        _configuration = configuration;
        _services = services;
    }
    
    public async Task<string> GeneratePdf(FilterResumenSaldo filter)
    {
        var settings = _configuration.GetSection("Settings").Get<Settings>();
        var result = "No Data";
        var pathLogo = @settings.BmFiles + "LogoIzquierda.jpeg";
        var fileName= $"ResumenSaldo-{filter.CodigoPresupuesto}.pdf";
        var filePath = $"{ @settings.ExcelFiles}/{fileName}.pdf";


        var resumen = await _services.GetAllByPresupuesto(filter.CodigoPresupuesto);
        if (resumen == null)
        {
            return "No Data";
        }
        else
        {
            var document = new ResumenSaldoDocument(resumen.Data,pathLogo);
            fileName= $"ResumenSaldo-{filter.CodigoPresupuesto}.pdf";
            filePath = $"{ @settings.ExcelFiles}/{fileName}";
            document.GeneratePdf(filePath);
            result =fileName;
          
        }
      
        return result;
    }
    
    
}