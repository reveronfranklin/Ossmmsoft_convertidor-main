using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntMayoresService
    {
        Task<ResultDto<List<CntMayoresResponseDto>>> GetAll();
        Task<ResultDto<CntMayoresResponseDto>> Create(CntMayoresUpdateDto dto);
        Task<ResultDto<CntMayoresResponseDto>> Update(CntMayoresUpdateDto dto);
        Task<ResultDto<CntMayoresDeleteDto>> Delete(CntMayoresDeleteDto dto);
    }
}
