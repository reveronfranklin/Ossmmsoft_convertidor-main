using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntTmpConciliacionService
    {
        Task<ResultDto<List<CntTmpConciliacionResponseDto>>> GetAll();
    }
}
