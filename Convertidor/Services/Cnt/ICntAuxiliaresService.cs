using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntAuxiliaresService
    {
        Task<ResultDto<List<CntAuxiliaresResponseDto>>> GetAll();
    }
}
