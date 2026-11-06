using QuestPDF.Fluent;

namespace Convertidor.Services.Rh.Report.Example;

public class ReportReciboPagoService : IReportReciboPagoService
{
    private readonly IConfiguration _configuration;
    private readonly IRhReporteNominaHistoricoService _historico;
    private readonly IRhTipoNominaService _tiposNomina;
    private readonly IRhReporteNominaTemporalService _temporal;
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<ReportReciboPagoService> _logger;

    public ReportReciboPagoService(IConfiguration configuration,
        IRhReporteNominaHistoricoService historico, IRhTipoNominaService tiposNomina,
        IRhReporteNominaTemporalService temporal, IWebHostEnvironment environment,
        ILogger<ReportReciboPagoService> logger)
    {
        _configuration = configuration;
        _historico = historico;
        _tiposNomina = tiposNomina;
        _temporal = temporal;
        _environment = environment;
        _logger = logger;
    }

    public async Task<ResultDto<byte[]>> GeneratePdf(FilterRepoteNomina filter)
    {
        ResultDto<byte[]> Fail(string message) => new(null) { IsValid = false, Message = message };
        if (filter.CodigoTipoNomina <= 0 || filter.CodigoPeriodo <= 0
            || filter.CodigoIcp < 0 || filter.CodigoPersona < 0)
            return Fail("Nomina, periodo o filtros de recibo invalidos.");
        // No existe un contrato productivo de destinatarios en este flujo.
        if (filter.SendEmail)
            return Fail("El envio por correo no esta habilitado para este reporte.");
        if (!int.TryParse(_configuration["settings:EmpresaConfig"], out var empresa) || empresa <= 0)
            return Fail("EmpresaConfig debe ser un numero valido.");

        try
        {
            // Copia local: generar no modifica los filtros del llamador.
            var query = new FilterRepoteNomina {
                CodigoTipoNomina = filter.CodigoTipoNomina, CodigoPeriodo = filter.CodigoPeriodo,
                CodigoIcp = filter.CodigoIcp, CodigoPersona = filter.CodigoPersona, SendEmail = false
            };
            var temporal = await _temporal.GetByPeriodoTipoNomina(query);
            if (temporal == null || !temporal.IsValid)
                return Fail("No se pudo consultar la nomina temporal para el recibo.");
            List<RhReporteNominaResponseDto> Scope(List<RhReporteNominaResponseDto> rows) =>
                (rows ?? new()).Where(x => x.CodigoEmpresa == empresa
                    && x.CodigoPeriodo == filter.CodigoPeriodo
                    && x.CodigoTipoNomina == filter.CodigoTipoNomina).ToList();
            var rows = Scope(temporal.Data);
            var isTemporal = rows.Count > 0;
            if (!isTemporal)
            {
                var historico = await _historico.GetByPeriodoTipoNomina(query);
                if (historico == null || !historico.IsValid)
                    return Fail("No se pudo consultar la nomina historica para el recibo.");
                rows = Scope(historico.Data);
            }
            if (filter.CodigoIcp > 0) rows = rows.Where(x => x.CodigoIcp == filter.CodigoIcp).ToList();
            if (filter.CodigoPersona > 0) rows = rows.Where(x => x.CodigoPersona == filter.CodigoPersona).ToList();
            if (rows.Count == 0) return Fail("No hay recibos para los filtros seleccionados.");

            var tipoNomina = await _tiposNomina.GetByCodigo(new RhTiposNominaFilterDto {
                CodigoTipoNomina = filter.CodigoTipoNomina
            });
            if (tipoNomina == null) return Fail("No se pudo consultar el tipo de nomina.");
            var logo = _configuration["settings:RhReciboLogo"];
            if (string.IsNullOrWhiteSpace(logo))
                logo = Path.Combine(_configuration["settings:BmFiles"] ?? "", "LogoIzquierda.jpeg");
            if (!Path.IsPathRooted(logo)) logo = Path.Combine(_environment.ContentRootPath, logo);
            if (!File.Exists(logo)) return Fail("No se encontro el logo del recibo. Verifique la configuracion del servidor.");

            // Se conserva la marca provisional de los lotes temporales del generador legacy.
            var document = new ReciboPagoDocument(rows, logo, tipoNomina.Descripcion,
                isTemporal && !(filter.CodigoPersona > 0));
            var pdf = document.GeneratePdf();
            return new ResultDto<byte[]>(pdf) { IsValid = true, Message = "success" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al generar recibo PDF. Nomina {Nomina}, periodo {Periodo}, persona {Persona}",
                filter.CodigoTipoNomina, filter.CodigoPeriodo, filter.CodigoPersona);
            return Fail("No se pudo generar el PDF del recibo. Revise los registros del servidor.");
        }
    }
}
