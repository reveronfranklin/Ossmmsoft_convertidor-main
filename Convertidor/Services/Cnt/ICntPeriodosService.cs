using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntPeriodosService
    {
        Task<ResultDto<List<CntPeriodosResponseDto>>> GetAll();
        Task<ResultDto<CntPeriodosResponseDto>> Create(CntPeriodosUpdateDto dto);
        Task<ResultDto<CntPeriodosResponseDto>> Update(CntPeriodosUpdateDto dto);
        Task<ResultDto<CntPeriodosDeleteDto>> Delete(CntPeriodosDeleteDto dto);
    }
}
