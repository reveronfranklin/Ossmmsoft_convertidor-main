using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteSolicitudModificacion;

namespace Convertidor.Services.Presupuesto.Reports.ReporteSolicitudModificacionPresupuestaria
{
    public interface IReporteSolicitudModificacionPresupuestariaService
    {
        Task<string> ReportData(int codigoSolModificacion, string dePara);
        
    }
}
