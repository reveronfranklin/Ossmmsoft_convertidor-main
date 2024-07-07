using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntSaldosService
    {
        Task<ResultDto<List<CntSaldosResponseDto>>> GetAll();
        Task<ResultDto<CntSaldosResponseDto>> Create(CntSaldosUpdateDto dto);
    }
}
