using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatDireccionesService
    {
        Task<ResultDto<List<CatDireccionesResponseDto>>> GetAll();
        Task<ResultDto<CatDireccionesResponseDto>> Create(CatDireccionesUpdateDto dto);
        Task<ResultDto<CatDireccionesResponseDto>> Update(CatDireccionesUpdateDto dto);
        Task<ResultDto<CatDireccionesDeleteDto>> Delete(CatDireccionesDeleteDto dto);
    }
}
