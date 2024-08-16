using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatDescriptivasService
    {
        Task<ResultDto<List<CatDescriptivasResponseDto>>> GetAll();
        Task<ResultDto<CatDescriptivasResponseDto>> Create(CatDescriptivasUpdateDto dto);
        Task<ResultDto<CatDescriptivasResponseDto>> Update(CatDescriptivasUpdateDto dto);
    }
}
