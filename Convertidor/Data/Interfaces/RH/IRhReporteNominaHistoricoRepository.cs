namespace Convertidor.Data.Interfaces.RH;

public interface IRhReporteNominaHistoricoRepository
{
    Task<List<RH_V_REPORTE_NOMINA_HISTORICO>> GetByPeriodoTipoNomina(int codigoPeriodo, int tipoNomina);
}