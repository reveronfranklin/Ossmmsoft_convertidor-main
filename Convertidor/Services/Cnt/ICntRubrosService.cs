using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntRubrosService
    {
        Task<ResultDto<List<CntRubrosResponseDto>>> GetAll();
        Task<ResultDto<CntRubrosResponseDto>> Create(CntRubrosUpdateDto dto);
        Task<ResultDto<CntRubrosResponseDto>> Update(CntRubrosUpdateDto dto);
        Task<ResultDto<CntRubrosDeleteDto>> Delete(CntRubrosDeleteDto dto);
    }
}
