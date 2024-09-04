using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatDValorConstruccionService
    {
        Task<ResultDto<List<CatDValorConstruccionResponseDto>>> GetAll();
        Task<ResultDto<CatDValorConstruccionResponseDto>> Create(CatDValorConstruccionUpdateDto dto);
        Task<ResultDto<CatDValorConstruccionResponseDto>> Update(CatDValorConstruccionUpdateDto dto);
    }
}
