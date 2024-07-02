using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntBalancesService
    {
        Task<ResultDto<List<CntBalancesResponseDto>>> GetAll();
        Task<ResultDto<CntBalancesResponseDto>> Create(CntBalancesUpdateDto dto);
        Task<ResultDto<CntBalancesResponseDto>> Update(CntBalancesUpdateDto dto);
    }
}
