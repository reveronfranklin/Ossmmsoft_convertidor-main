using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntMayoresService
    {
        Task<ResultDto<List<CntMayoresResponseDto>>> GetAll();
        Task<ResultDto<CntMayoresResponseDto>> Create(CntMayoresUpdateDto dto);
    }
}
