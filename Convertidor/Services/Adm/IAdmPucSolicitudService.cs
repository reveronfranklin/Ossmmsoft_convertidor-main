using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmPucSolicitudService
    {
        Task<ResultDto<List<AdmPucSolicitudResponseDto>>> GetAll();
        Task<ResultDto<AdmPucSolicitudResponseDto>> Update(AdmPucSolicitudUpdateDto dto);
        Task<ResultDto<AdmPucSolicitudResponseDto>> Create(AdmPucSolicitudUpdateDto dto);
        Task<ResultDto<AdmPucSolicitudDeleteDto>> Delete(AdmPucSolicitudDeleteDto dto);
    }
}
