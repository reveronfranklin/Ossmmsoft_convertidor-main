using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ITmpLibrosService
    {
        Task<ResultDto<List<TmpLibrosResponseDto>>> GetAll();
        Task<ResultDto<TmpLibrosResponseDto>> Create(TmpLibrosUpdateDto dto);
        Task<ResultDto<TmpLibrosResponseDto>> Update(TmpLibrosUpdateDto dto);
        Task<ResultDto<TmpLibrosDeleteDto>> Delete(TmpLibrosDeleteDto dto);
    }
}
