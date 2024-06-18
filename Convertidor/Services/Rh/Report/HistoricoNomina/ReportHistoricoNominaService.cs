
using QuestPDF.Fluent;

namespace Convertidor.Services.Rh.Report.HistoricoNomina;

public class ReportHistoricoNominaService:IReportHistoricoNominaService
{
    private readonly IConfiguration _configuration;
    private readonly IRhReporteNominaHistoricoService _rhReporteNominaHistoricoService;
    private readonly IRhReporteNominaTemporalService _rhReporteNominaTemporalService;
    private readonly IRhReporteFirmaService _rhReporteFirmaService;
    private readonly IRhPeriodoService _rhPeriodoService;


    public ReportHistoricoNominaService(IConfiguration configuration,
                                        IRhReporteNominaHistoricoService rhReporteNominaHistoricoService,
                                        IRhReporteNominaTemporalService rhReporteNominaTemporalService,
                                        IRhReporteFirmaService rhReporteFirmaService,
                                        IRhPeriodoService rhPeriodoService)
    {
        _configuration = configuration;
        _rhReporteNominaHistoricoService = rhReporteNominaHistoricoService;
        _rhReporteNominaTemporalService = rhReporteNominaTemporalService;
        _rhReporteFirmaService = rhReporteFirmaService;
        _rhPeriodoService = rhPeriodoService;
    }
    
    public async Task<string> GeneratePdf(FilterRepoteNomina filter)
    {
        var result = "No Data";
        try
        {
                var settings = _configuration.GetSection("Settings").Get<Settings>();
        
        var pathLogo = @settings.BmFiles + "LogoIzquierda.jpeg";
        var fileName= $"HistoricoDeNomina-{filter.CodigoTipoNomina}-{filter.CodigoPeriodo}.pdf";
        var filePath = $"{ @settings.ExcelFiles}/{fileName}.pdf";


        var periodo = await _rhPeriodoService.GetByPeriodo(filter.CodigoPeriodo);
        if (periodo == null)
        {
            return "No Data";
        }
        else
        {
            var model = await   _rhReporteNominaHistoricoService.GetByPeriodoTipoNominaResumenConcepto(filter);
            if (model.Data!=null)
            {
                //var model = await   _rhReporteNominaHistoricoService.GetByPeriodoTipoNominaResumenConcepto(filter);

                var modelFirma = await _rhReporteFirmaService.GetAll();
                if (modelFirma == null)
                {
                    return "No Data Firma";
                }
                var modelRecibos = await _rhReporteNominaHistoricoService.GetByPeriodoTipoNomina(filter);
                var document = new HistoricoNominaDocument(model.Data,modelFirma.Data,modelRecibos.Data,pathLogo);
                fileName= $"HistoricoDeNomina-{filter.CodigoTipoNomina}-{filter.CodigoPeriodo}.pdf";
                filePath = $"{ @settings.ExcelFiles}/{fileName}";
                document.GeneratePdf(filePath);
                result =fileName;
            }
            else
            {
                model = await   _rhReporteNominaTemporalService.GetByPeriodoTipoNominaResumenConcepto(filter);
                if (model.Data == null) return model.Message;
        
                var modelFirma = await _rhReporteFirmaService.GetAll();
                if (modelFirma == null) return "No Data";
                
                var modelRecibos = await _rhReporteNominaTemporalService.GetByPeriodoTipoNomina(filter);
                if (modelRecibos.Data == null) return "No Data";
                var document = new HistoricoNominaDocument(model.Data,modelFirma.Data,modelRecibos.Data,pathLogo);
                fileName= $"TemporalDeNomina-{filter.CodigoTipoNomina}-{filter.CodigoPeriodo}.pdf";
                filePath = $"{ @settings.ExcelFiles}/{fileName}";
                document.GeneratePdf(filePath);
                result =fileName;
            }
        }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return result;
        }
        
    
      
        return result;
    }
    
    
}