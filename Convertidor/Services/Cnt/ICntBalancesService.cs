using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntBalancesService
    {
        Task<ResultDto<List<CntBalancesResponseDto>>> GetAll();
    }
}
