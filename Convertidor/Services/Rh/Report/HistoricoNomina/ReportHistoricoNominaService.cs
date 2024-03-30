
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
namespace Convertidor.Services.Rh.Report.Example;

public class ReportHistoricoNominaService:IReportHistoricoNominaService
{
    private readonly IConfiguration _configuration;
    private readonly IRhReporteNominaHistoricoService _rhReporteNominaHistoricoService;


    public ReportHistoricoNominaService(IConfiguration configuration,IRhReporteNominaHistoricoService rhReporteNominaHistoricoService)
    {
        _configuration = configuration;
        _rhReporteNominaHistoricoService = rhReporteNominaHistoricoService;
     
    }
    
    public async Task GeneratePdf(FilterRepoteNomina filter)
    {
        var settings = _configuration.GetSection("Settings").Get<Settings>();
        
        var pathLogo = @settings.BmFiles + "LogoIzquierda.jpeg";
        var filePath = $"HistoricoDeNomina-{filter.CodigoTipoNomina}-{filter.CodigoPeriodo}.pdf";


        var model = await   _rhReporteNominaHistoricoService.GetByPeriodoTipoNominaResumenConcepto(filter);
        var document = new HistoricoNominaDocument(model.Data,pathLogo);
        document.GeneratePdf(filePath);
    }
    
    
}