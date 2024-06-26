using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntLibrosService
    {
        Task<ResultDto<List<CntLibrosResponseDto>>> GetAll();
    }
}
