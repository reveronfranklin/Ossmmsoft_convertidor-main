using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ITmpLibrosService
    {
        Task<ResultDto<List<TmpLibrosResponseDto>>> GetAll();
        Task<ResultDto<TmpLibrosResponseDto>> Create(TmpLibrosUpdateDto dto);
    }
}
