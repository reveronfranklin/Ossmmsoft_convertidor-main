using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntMayoresService
    {
        Task<ResultDto<List<CntMayoresResponseDto>>> GetAll();
    }
}
