using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatDValorTierraService
    {
        Task<ResultDto<List<CatDValorTierraResponseDto>>> GetAll();
        Task<ResultDto<CatDValorTierraResponseDto>> Create(CatDValorTierraUpdateDto dto);
    }
}
