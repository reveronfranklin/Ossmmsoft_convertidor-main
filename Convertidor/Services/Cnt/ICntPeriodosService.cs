using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntPeriodosService
    {
        Task<ResultDto<List<CntPeriodosResponseDto>>> GetAll();
        Task<ResultDto<CntPeriodosResponseDto>> Create(CntPeriodosUpdateDto dto);
    }
}
