using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmOrdenPagoService
    {
        Task<ResultDto<List<AdmOrdenPagoResponseDto>>> GetByPresupuesto(AdmOrdenPagoFilterDto filter);
        Task<ResultDto<AdmOrdenPagoResponseDto>> Update(AdmOrdenPagoUpdateDto dto);
        Task<ResultDto<AdmOrdenPagoResponseDto>> Create(AdmOrdenPagoUpdateDto dto);
        Task<ResultDto<AdmOrdenPagoDeleteDto>> Delete(AdmOrdenPagoDeleteDto dto);
        Task<ResultDto<AdmOrdenPagoResponseDto>> Aprobar(AdmOrdenPagoAprobarAnular dto);
        Task<ResultDto<AdmOrdenPagoResponseDto>> Anular(AdmOrdenPagoAprobarAnular dto);
        Task<decimal> GetMontoPagado(int codigoOrdenPago);

    }
}
