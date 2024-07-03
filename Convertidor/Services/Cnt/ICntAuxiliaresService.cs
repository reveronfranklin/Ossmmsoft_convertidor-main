using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntAuxiliaresService
    {
        Task<ResultDto<List<CntAuxiliaresResponseDto>>> GetAll();
        Task<ResultDto<List<CntAuxiliaresResponseDto>>> GetAllByCodigoMayor(int codigoMayor);
    }
}
