using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntBancoArchivoService
    {
        Task<ResultDto<List<CntBancoArchivoResponseDto>>> GetAll();
        Task<ResultDto<CntBancoArchivoResponseDto>> Create(CntBancoArchivoUpdateDto dto);
    }
}
