using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.Pagos;

namespace Convertidor.Services.Adm.Pagos;

public interface IAdmPagosService
{
    Task<ResultDto<List<PagoResponseDto>>> GetByLote(AdmChequeFilterDto dto);
    Task<ResultDto<PagoResponseDto>> Create(PagoCreateDto dto);
    Task<ResultDto<bool>> UpdateMonto(PagoUpdateMontoDto dto);
    Task<ResultDto<PagoResponseDto>> Update(PagoUpdateDto dto);
    Task<ResultDto<bool>> Delete(PagoDeleteDto dto);
}