
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
namespace Convertidor.Services.Rh.Report.Example;

public class ReportHistoricoNominaService:IReportHistoricoNominaService
{
    private readonly IConfiguration _configuration;
    private readonly IRhReporteNominaHistoricoService _rhReporteNominaHistoricoService;
    private readonly IRhReporteFirmaService _rhReporteFirmaService;


    public ReportHistoricoNominaService(IConfiguration configuration,
                                        IRhReporteNominaHistoricoService rhReporteNominaHistoricoService,
                                        IRhReporteFirmaService rhReporteFirmaService)
    {
        _configuration = configuration;
        _rhReporteNominaHistoricoService = rhReporteNominaHistoricoService;
        _rhReporteFirmaService = rhReporteFirmaService;
    }
    
    public async Task GeneratePdf(FilterRepoteNomina filter)
    {
        var settings = _configuration.GetSection("Settings").Get<Settings>();
        
        var pathLogo = @settings.BmFiles + "LogoIzquierda.jpeg";
        var filePath = $"HistoricoDeNomina-{filter.CodigoTipoNomina}-{filter.CodigoPeriodo}.pdf";

    
        var model = await   _rhReporteNominaHistoricoService.GetByPeriodoTipoNominaResumenConcepto(filter);
        var modelFirma = await _rhReporteFirmaService.GetAll();
        var modelRecibos = await _rhReporteNominaHistoricoService.GetByPeriodoTipoNomina(filter);
        var document = new HistoricoNominaDocument(model.Data,modelFirma.Data,modelRecibos.Data,pathLogo);
        document.GeneratePdf(filePath);
    }
    
    
}