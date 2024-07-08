using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntComprobantesService
    {
        Task<ResultDto<List<CntComprobantesResponseDto>>> GetAll();
        Task<ResultDto<CntComprobantesResponseDto>> Create(CntComprobantesUpdateDto dto);
    }
}
