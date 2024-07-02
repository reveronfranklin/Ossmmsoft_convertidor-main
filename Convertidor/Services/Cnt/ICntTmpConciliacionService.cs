using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntTmpConciliacionService
    {
        Task<ResultDto<List<CntTmpConciliacionResponseDto>>> GetAll();
        Task<ResultDto<CntTmpConciliacionResponseDto>> Create(CntTmpConciliacionUpdateDto dto);
        Task<ResultDto<CntTmpConciliacionResponseDto>> Update(CntTmpConciliacionUpdateDto dto);
    }
}
