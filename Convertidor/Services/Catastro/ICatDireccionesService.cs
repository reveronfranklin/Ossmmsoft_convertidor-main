using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatDireccionesService
    {
        Task<ResultDto<List<CatDireccionesResponseDto>>> GetAll();
        Task<ResultDto<CatDireccionesResponseDto>> Create(CatDireccionesUpdateDto dto);
    }
}
