using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntReversoConciliacionService
    {
        
        Task<ResultDto<List<CntReversoConciliacionResponseDto>>> GetAllByCodigoConciliacion(int codigoConciliacion);
        Task<ResultDto<CntReversoConciliacionResponseDto>> Create(CntReversoConciliacionUpdateDto dto);
        Task<ResultDto<CntReversoConciliacionResponseDto>> Update(CntReversoConciliacionUpdateDto dto);
        Task<ResultDto<CntReversoConciliacionDeleteDto>> Delete(CntReversoConciliacionDeleteDto dto);
    }
}
