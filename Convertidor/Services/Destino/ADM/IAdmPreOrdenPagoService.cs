using Convertidor.Dtos.Adm.PreOrdenPago;

namespace Convertidor.Services.Destino.ADM;

public interface IAdmPreOrdenPagoService
{

    Task<ResultDto<AdmPreOdenPagoResponseDto>> Create(AdmPreOdenPagoCreateDto dto);
    Task<ResultDto<bool>> CreateLote(List<AdmPreOdenPagoCreateDto> dto);
    Task<ResultDto<List<AdmPreOdenPagoResponseDto>>> GetAll(AdmPreOrdenPagoFilterDto dto);
    Task<ResultDto<bool>> DeleteAll();
}