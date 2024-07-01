using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntRubrosService
    {
        Task<ResultDto<List<CntRubrosResponseDto>>> GetAll();
    }
}
