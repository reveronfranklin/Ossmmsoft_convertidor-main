using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto.Reports.ReporteOrdenSercicioPresupuestario
{
    public interface IReporteOrdenServicioPresupuestarioService
    {
        Task<string> ReportData(FilterReporteBySolicitud filter);
    }
}
