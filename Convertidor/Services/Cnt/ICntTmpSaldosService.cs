using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntTmpSaldosService
    {
        Task<ResultDto<List<CntTmpSaldosResponseDto>>> GetAll();
        Task<ResultDto<CntTmpSaldosResponseDto>> Create(CntTmpSaldosUpdateDto dto);
    }
}
