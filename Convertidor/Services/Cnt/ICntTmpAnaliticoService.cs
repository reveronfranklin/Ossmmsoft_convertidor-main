using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntTmpAnaliticoService
    {
        Task<ResultDto<List<CntTmpAnaliticoResponseDto>>> GetAll();
        Task<ResultDto<CntTmpAnaliticoResponseDto>> Create(CntTmpAnaliticoUpdateDto dto);
        Task<ResultDto<CntTmpAnaliticoResponseDto>> Update(CntTmpAnaliticoUpdateDto dto);
        Task<ResultDto<CntTmpAnaliticoDeleteDto>> Delete(CntTmpAnaliticoDeleteDto dto);
    }
}
