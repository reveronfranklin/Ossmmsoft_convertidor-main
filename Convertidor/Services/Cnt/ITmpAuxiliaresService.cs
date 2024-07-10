using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ITmpAuxiliaresService
    {
        Task<ResultDto<List<TmpAuxiliaresResponseDto>>> GetAll();
        Task<ResultDto<TmpAuxiliaresResponseDto>> Create(TmpAuxiliaresUpdateDto dto);
    }
}
