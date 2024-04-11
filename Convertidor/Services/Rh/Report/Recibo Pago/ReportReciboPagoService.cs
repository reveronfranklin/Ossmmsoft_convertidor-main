
using Convertidor.Data.Repository.Rh;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
namespace Convertidor.Services.Rh.Report.Example;

public class ReportReciboPagoService : IReportReciboPagoService
{
    private readonly IConfiguration _configuration;
    private readonly IRhReporteNominaHistoricoService _rhReporteNominaHistoricoService;
    private readonly IRhTipoNominaService _rhTipoNominaService;


    public ReportReciboPagoService(IConfiguration configuration,
                                        IRhReporteNominaHistoricoService rhReporteNominaHistoricoService,
                                        IRhTipoNominaService rhTipoNominaService)
    {
        _configuration = configuration;
        _rhReporteNominaHistoricoService = rhReporteNominaHistoricoService;
        _rhTipoNominaService = rhTipoNominaService;
      
    }

    public async Task GeneratePdf(FilterRepoteNomina filter)
    {
        var settings = _configuration.GetSection("Settings").Get<Settings>();

        var pathLogo = @settings.BmFiles + "LogoIzquierda.jpeg";
        var filePath = $"{@settings.RhFiles}ReporteReciboPago-{filter.CodigoTipoNomina}-{filter.CodigoPeriodo}.pdf";
        

        RhTiposNominaFilterDto filterTipoNomina = new RhTiposNominaFilterDto();
        filterTipoNomina.CodigoTipoNomina = filter.CodigoTipoNomina;
        var tipoNomina = await _rhTipoNominaService.GetByCodigo(filterTipoNomina);
        
        
        

            if (filter.CodigoPersona > 0)
            {
                var modelRecibos = await _rhReporteNominaHistoricoService.GetByPeriodoTipoNominaPersona(filter);
                var document = new ReciboPagoDocument(modelRecibos.Data, pathLogo, tipoNomina.Descripcion,filter.ImprimirMarcaAgua);
                document.GeneratePdf(filePath);
            }
            else
            {
                var modelRecibos = await _rhReporteNominaHistoricoService.GetByPeriodoTipoNomina(filter);
                var document = new ReciboPagoDocument(modelRecibos.Data, pathLogo, tipoNomina.Descripcion, filter.ImprimirMarcaAgua);
                document.GeneratePdf(filePath);
            }
        
    }
}