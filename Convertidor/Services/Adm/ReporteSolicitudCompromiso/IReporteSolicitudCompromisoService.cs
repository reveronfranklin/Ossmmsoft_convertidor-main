using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.ReporteSolicitudCompromiso
{
    public interface IReporteSolicitudCompromisoService
    {
        Task<string> ReportData(AdmSolicitudesFilterDto filter);
    }
}
