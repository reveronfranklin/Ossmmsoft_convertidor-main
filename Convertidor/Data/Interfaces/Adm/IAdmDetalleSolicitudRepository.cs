using Convertidor.Data.Entities.ADM;
using Convertidor.Dtos.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmDetalleSolicitudRepository
    {
        Task<ADM_DETALLE_SOLICITUD> GetCodigoDetalleSolicitud(int codigoDetalleSolicitud);
        Task<List<ADM_DETALLE_SOLICITUD>> GetAll();
        List<AdmDetalleSolicitudResponseDto> GetByCodigoSolicitud(int codigoSolicitud);
        Task<ResultDto<ADM_DETALLE_SOLICITUD>> Add(ADM_DETALLE_SOLICITUD entity);
        Task<ResultDto<ADM_DETALLE_SOLICITUD>> Update(ADM_DETALLE_SOLICITUD entity);
        Task<string> Delete(int codigoSolicitud);
        Task<int> GetNextKey();
    }
}
