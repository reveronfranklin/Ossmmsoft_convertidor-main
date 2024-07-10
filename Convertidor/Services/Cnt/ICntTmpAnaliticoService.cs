using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntTmpAnaliticoService
    {
        Task<ResultDto<List<CntTmpAnaliticoResponseDto>>> GetAll();
        Task<ResultDto<CntTmpAnaliticoResponseDto>> Create(CntTmpAnaliticoUpdateDto dto);
    }
}
