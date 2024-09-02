using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmPucOrdenPagoService
    {
        Task<ResultDto<List<AdmPucOrdenPagoResponseDto>>> GetAll();
        Task<ResultDto<AdmPucOrdenPagoResponseDto>> Update(AdmPucOrdenPagoUpdateDto dto);
        Task<ResultDto<AdmPucOrdenPagoResponseDto>> Create(AdmPucOrdenPagoUpdateDto dto);
        Task<ResultDto<AdmPucOrdenPagoDeleteDto>> Delete(AdmPucOrdenPagoDeleteDto dto);
        Task<ResultDto<List<AdmPucOrdenPagoResponseDto>>> GetByOrdenPago(int codigoOrdenPago);
        
    }
}
