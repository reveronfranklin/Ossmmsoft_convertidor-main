namespace Convertidor.Services.Rh;

public interface IRhReporteNominaHistoricoService
{

    Task<ResultDto<List<RhReporteNominaResponseDto>>> GetByPeriodoTipoNomina(FilterRepoteNomina filter);
    Task<ResultDto<List<RhReporteNominaResponseDto>>> GetByPeriodoTipoNominaPersona(FilterRepoteNomina filter);

    Task<ResultDto<List<RhReporteNominaResumenConceptoResponseDto>>> GetByPeriodoTipoNominaResumenConcepto(
        FilterRepoteNomina filter);

    Task<ResultDto<List<RhReporteNominaResumenConceptoResponseDto>>> GetByPeriodoTipoNominaResumenConceptoPersona(
        FilterRepoteNomina filter);
}