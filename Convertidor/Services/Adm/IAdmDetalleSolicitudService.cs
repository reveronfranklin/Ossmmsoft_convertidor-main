using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmDetalleSolicitudService
    {
        Task<ResultDto<List<AdmDetalleSolicitudResponseDto>>> GetAll();
        Task<ResultDto<AdmDetalleSolicitudResponseDto>> Update(AdmDetalleSolicitudUpdateDto dto);
        Task<ResultDto<AdmDetalleSolicitudResponseDto>> Create(AdmDetalleSolicitudUpdateDto dto);
        Task<ResultDto<AdmDetalleSolicitudDeleteDto>> Delete(AdmDetalleSolicitudDeleteDto dto);
    }
}
