using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntTmpHistAnaliticoService
    {
        Task<ResultDto<List<CntTmpHistAnaliticoResponseDto>>> GetAll();
    }
}
