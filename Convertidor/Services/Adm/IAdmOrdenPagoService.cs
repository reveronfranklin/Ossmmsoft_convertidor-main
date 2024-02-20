using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmOrdenPagoService
    {
        
        Task<ResultDto<AdmOrdenPagoResponseDto>> Update(AdmOrdenPagoUpdateDto dto);
        Task<ResultDto<AdmOrdenPagoResponseDto>> Create(AdmOrdenPagoUpdateDto dto);
        Task<ResultDto<AdmOrdenPagoDeleteDto>> Delete(AdmOrdenPagoDeleteDto dto);
    }
}
