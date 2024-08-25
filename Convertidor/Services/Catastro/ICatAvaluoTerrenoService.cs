using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatAvaluoTerrenoService
    {
        Task<ResultDto<List<CatAvaluoTerrenoResponseDto>>> GetAll();
        Task<ResultDto<CatAvaluoTerrenoResponseDto>> Create(CatAvaluoTerrenoUpdateDto dto);
        Task<ResultDto<CatAvaluoTerrenoResponseDto>> Update(CatAvaluoTerrenoUpdateDto dto);
    }
}
