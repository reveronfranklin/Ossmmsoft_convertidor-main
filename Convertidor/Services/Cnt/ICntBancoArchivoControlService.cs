using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntBancoArchivoControlService
    {
        Task<ResultDto<List<CntBancoArchivoControlResponseDto>>> GetAll();
        Task<ResultDto<CntBancoArchivoControlResponseDto>> Create(CntBancoArchivoControlUpdateDto dto);
        Task<ResultDto<CntBancoArchivoControlResponseDto>> Update(CntBancoArchivoControlUpdateDto dto);
        Task<ResultDto<CntBancoArchivoControlDeleteDto>> Delete(CntBancoArchivoControlDeleteDto dto);
    }
}
