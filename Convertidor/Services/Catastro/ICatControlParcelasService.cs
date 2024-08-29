using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatControlParcelasService
    {
        Task<ResultDto<List<CatControlParcelasResponseDto>>> GetAll();
        Task<ResultDto<CatControlParcelasResponseDto>> Create(CatControlParcelasUpdateDto dto);
        Task<ResultDto<CatControlParcelasResponseDto>> Update(CatControlParcelasUpdateDto dto);
    }
}
