using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntTmpHistAnaliticoService
    {
        Task<ResultDto<List<CntTmpHistAnaliticoResponseDto>>> GetAll();
        Task<ResultDto<CntTmpHistAnaliticoResponseDto>> Create(CntTmpHistAnaliticoUpdateDto dto);
        Task<ResultDto<CntTmpHistAnaliticoResponseDto>> Update(CntTmpHistAnaliticoUpdateDto dto);
    }
}
