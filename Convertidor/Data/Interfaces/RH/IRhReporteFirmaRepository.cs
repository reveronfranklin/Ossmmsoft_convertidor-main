namespace Convertidor.Data.Interfaces.RH;

public interface IRhReporteFirmaRepository
{
    Task<List<RH_V_REPORTE_NOMINA_FIRMA>> GetAll();
}