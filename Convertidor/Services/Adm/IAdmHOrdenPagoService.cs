using Convertidor.Dtos.Adm;
using Convertidor.Dtos;

namespace Convertidor.Services.Adm
{
    public interface IAdmHOrdenPagoService
    {
        Task<ResultDto<List<AdmHOrdenPagoResponseDto>>> GetAll();
        Task<ResultDto<AdmHOrdenPagoResponseDto>> Update(AdmHOrdenPagoUpdateDto dto);
        Task<ResultDto<AdmHOrdenPagoResponseDto>> Create(AdmHOrdenPagoUpdateDto dto);
        Task<ResultDto<AdmHOrdenPagoDeleteDto>> Delete(AdmHOrdenPagoDeleteDto dto);
    }
}
