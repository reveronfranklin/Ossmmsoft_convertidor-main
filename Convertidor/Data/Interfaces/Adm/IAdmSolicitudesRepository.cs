using Convertidor.Data.Entities.ADM;
using Convertidor.Dtos.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmSolicitudesRepository
    {
        Task<ADM_SOLICITUDES> GetByCodigoSolicitud(int codigoSolicitud);

        Task<ResultDto<List<AdmSolicitudesResponseDto>>> GetByPresupuesto(AdmSolicitudesFilterDto filter);
        Task<ResultDto<ADM_SOLICITUDES>> Add(ADM_SOLICITUDES entity);
        Task<ResultDto<ADM_SOLICITUDES>> Update(ADM_SOLICITUDES entity);
        Task<string> Delete(int codigoSolicitud);
        Task<int> GetNextKey();
        Task<string> UpdateStatus(int codigoSolicitud, string status);
    }
}
