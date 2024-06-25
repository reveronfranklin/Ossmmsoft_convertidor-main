using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntHistConciliacionService
    {
        Task<ResultDto<List<CntHistConciliacionResponseDto>>> GetAll();
    }
}
