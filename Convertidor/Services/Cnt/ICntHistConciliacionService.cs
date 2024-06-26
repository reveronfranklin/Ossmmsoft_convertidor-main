using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntHistConciliacionService
    {
        Task<ResultDto<List<CntHistConciliacionResponseDto>>> GetAll();
        Task<ResultDto<List<CntHistConciliacionResponseDto>>> GetAllByCodigoConciliacion(int codigoConciliacion);
        Task<ResultDto<CntHistConciliacionResponseDto>> Create(CntHistConciliacionUpdateDto dto);
        Task<ResultDto<CntHistConciliacionResponseDto>> Update(CntHistConciliacionUpdateDto dto);
        
    }
}
