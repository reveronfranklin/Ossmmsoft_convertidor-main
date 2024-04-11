namespace Convertidor.Data.Interfaces.RH;

public interface IRhReporteNominaTemporalRepository
{
    Task<List<RH_V_REPORTE_NOMINA_TEMPORAL>> GetByPeriodoTipoNomina(int codigoPeriodo, int tipoNomina);

    Task<List<RH_V_REPORTE_NOMINA_TEMPORAL>> GetByPeriodoTipoNominaPersona(int codigoPeriodo, int tipoNomina,
        int codigoPersona);
}