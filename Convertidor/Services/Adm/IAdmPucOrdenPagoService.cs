using Convertidor.Dtos.Adm;
using Convertidor.Dtos;

namespace Convertidor.Services.Adm
{
    public interface IAdmPucOrdenPagoService
    {
        Task<ResultDto<AdmPucOrdenPagoResponseDto>> Update(AdmPucOrdenPagoUpdateDto dto);
        Task<ResultDto<AdmPucOrdenPagoResponseDto>> Create(AdmPucOrdenPagoUpdateDto dto);
        Task<ResultDto<AdmPucOrdenPagoDeleteDto>> Delete(AdmPucOrdenPagoDeleteDto dto);
    }
}
