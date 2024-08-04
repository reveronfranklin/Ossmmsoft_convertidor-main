using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmSolicitudesService
    {
        Task<ResultDto<List<AdmSolicitudesResponseDto>>> GetByPresupuesto(AdmSolicitudesFilterDto filter);
        Task<ResultDto<List<AdmSolicitudesResponseDto>>> GetByPresupuestoPendiente(AdmSolicitudesFilterDto filter);
        Task<ResultDto<AdmSolicitudesResponseDto>> Update(AdmSolicitudesUpdateDto dto);
        Task<ResultDto<AdmSolicitudesResponseDto>> Create(AdmSolicitudesUpdateDto dto);
        Task<ResultDto<AdmSolicitudesDeleteDto>> Delete(AdmSolicitudesDeleteDto dto);
        Task<ResultDto<bool>> UpdateStatus(AdmSolicitudesUpdateDto dto);
        Task<ResultDto<bool>> SolicitudPuedeSerAprobada(int codigoSolicitud);
    }
}
