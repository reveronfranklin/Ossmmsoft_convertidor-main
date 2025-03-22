using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm;

public interface IAdmLotePagoService
{
    Task<ResultDto<AdmLotePagoResponseDto>> Update(AdmLotePagoUpdateDto dto);
    Task<ResultDto<AdmLotePagoResponseDto>> Create(AdmLotePagoUpdateDto dto);
    Task<ResultDto<AdmLotePagoDeleteDto>> Delete(AdmLotePagoDeleteDto dto);
    Task<ResultDto<List<AdmLotePagoResponseDto>>> GetAll(AdmLotePagoFilterDto filter);
    Task<ResultDto<AdmLotePagoResponseDto>> CambioStatus(AdmLotePagoCambioStatusDto dto);

}