using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntBancoArchivoControlService
    {
        Task<ResultDto<List<CntBancoArchivoControlResponseDto>>> GetAll();
        Task<ResultDto<CntBancoArchivoControlResponseDto>> Create(CntBancoArchivoControlUpdateDto dto);
    }
}
