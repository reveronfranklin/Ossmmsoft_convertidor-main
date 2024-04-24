using QuestPDF.Fluent;

namespace Convertidor.Services.Rh.Report.Example;

public class ReportReciboPagoService : IReportReciboPagoService
{
    private readonly IConfiguration _configuration;
    private readonly IRhReporteNominaHistoricoService _rhReporteNominaHistoricoService;
    private readonly IRhTipoNominaService _rhTipoNominaService;
    private readonly IRhReporteNominaTemporalService _serviceTemporal;


    public ReportReciboPagoService(IConfiguration configuration,
                                        IRhReporteNominaHistoricoService rhReporteNominaHistoricoService,
                                        IRhTipoNominaService rhTipoNominaService,
                                        IRhReporteNominaTemporalService serviceTemporal)
    {
        _configuration = configuration;
        _rhReporteNominaHistoricoService = rhReporteNominaHistoricoService;
        _rhTipoNominaService = rhTipoNominaService;
        _serviceTemporal = serviceTemporal;
    }

    public async Task GeneratePdf(FilterRepoteNomina filter)
    {
        var settings = _configuration.GetSection("Settings").Get<Settings>();

        var pathLogo = @settings.BmFiles + "LogoIzquierda.jpeg";
        var filePath = $"{@settings.ExcelFiles}/ReporteReciboPago-{filter.CodigoTipoNomina}-{filter.CodigoPeriodo}.pdf";
        ResultDto < List < RhReporteNominaResponseDto >> modelRecibos =
            new ResultDto<List<RhReporteNominaResponseDto>>(null);

        RhTiposNominaFilterDto filterTipoNomina = new RhTiposNominaFilterDto();
        filterTipoNomina.CodigoTipoNomina = filter.CodigoTipoNomina;
        var tipoNomina = await _rhTipoNominaService.GetByCodigo(filterTipoNomina);
            
        var temporal = await _serviceTemporal.GetByPeriodoTipoNomina(filter);
        if (temporal.Data!=null && temporal.Data.Count > 0)
        {
            if (filter.CodigoIcp!= null && filter.CodigoIcp > 0)
            {
                modelRecibos.Data = temporal.Data.Where(x => x.CodigoIcp == filter.CodigoIcp).ToList();
                filter.ImprimirMarcaAgua = true;
            }
            else
            {
                modelRecibos.Data = temporal.Data;
                filter.ImprimirMarcaAgua = true;
            }
            if (filter.CodigoPersona != null && filter.CodigoPersona > 0)
            {
                modelRecibos.Data= temporal.Data.Where(x => x.CodigoIcp == filter.CodigoIcp && x.CodigoPersona==filter.CodigoPersona).ToList();
                filter.ImprimirMarcaAgua = false;   
            }
            modelRecibos = temporal;
        }
        else
        {
            var historico = await _rhReporteNominaHistoricoService.GetByPeriodoTipoNomina(filter);
            if (filter.CodigoIcp!= null && filter.CodigoIcp > 0)
            {
                modelRecibos.Data= historico.Data.Where(x => x.CodigoIcp == filter.CodigoIcp).ToList();
                filter.ImprimirMarcaAgua = false;
            }
            else
            {
                modelRecibos.Data = historico.Data;
                filter.ImprimirMarcaAgua = false;
            }

            if (filter.CodigoPersona != null && filter.CodigoPersona > 0)
            {
                modelRecibos.Data= historico.Data.Where(x => x.CodigoIcp == filter.CodigoIcp && x.CodigoPersona==filter.CodigoPersona).ToList();
                filter.ImprimirMarcaAgua = false;   
            }

        }
        var document = new ReciboPagoDocument(modelRecibos.Data, pathLogo, tipoNomina.Descripcion,filter.ImprimirMarcaAgua);
        document.GeneratePdf(filePath);

           
        
    }
}