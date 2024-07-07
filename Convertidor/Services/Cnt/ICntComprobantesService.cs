using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntComprobantesService
    {
        Task<ResultDto<List<CntComprobantesResponseDto>>> GetAll();
    }
}
