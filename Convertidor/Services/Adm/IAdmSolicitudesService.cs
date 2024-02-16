using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmSolicitudesService
    {
        Task<ResultDto<AdmSolicitudesResponseDto>> Update(AdmSolicitudesUpdateDto dto);
        Task<ResultDto<AdmSolicitudesResponseDto>> Create(AdmSolicitudesUpdateDto dto);
        Task<ResultDto<AdmSolicitudesDeleteDto>> Delete(AdmSolicitudesDeleteDto dto);
    }
}
