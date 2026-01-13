using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto.Reports.ReporteCompromisoPresupuestario
{
    public interface IReporteCompromisoPresupuestarioService
    {
        Task<string> ReportData(FilterPreCompromisosDto filter);
    }
}
