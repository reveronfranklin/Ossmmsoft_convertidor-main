using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatTitulosService
    {
        Task<ResultDto<List<CatTitulosResponseDto>>> GetAll();
        Task<ResultDto<CatTitulosResponseDto>> Create(CatTitulosUpdateDto dto);
        Task<ResultDto<CatTitulosResponseDto>> Update(CatTitulosUpdateDto dto);
    }
}
