using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntLibrosService
    {
        Task<ResultDto<List<CntLibrosResponseDto>>> GetAll();
        Task<ResultDto<CntLibrosResponseDto>> Create(CntLibrosUpdateDto dto);
        Task<ResultDto<CntLibrosResponseDto>> Update(CntLibrosUpdateDto dto);
        Task<ResultDto<CntLibrosDeleteDto>> Delete(CntLibrosDeleteDto dto);
    }
}
