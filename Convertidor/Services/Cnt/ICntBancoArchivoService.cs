using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntBancoArchivoService
    {
        Task<ResultDto<List<CntBancoArchivoResponseDto>>> GetAll();
        Task<ResultDto<CntBancoArchivoResponseDto>> Create(CntBancoArchivoUpdateDto dto);
        Task<ResultDto<CntBancoArchivoResponseDto>> Update(CntBancoArchivoUpdateDto dto);
        Task<ResultDto<CntBancoArchivoDeleteDto>> Delete(CntBancoArchivoDeleteDto dto);
    }
}
