using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatAvaluoTerrenoService
    {
        Task<ResultDto<List<CatAvaluoTerrenoResponseDto>>> GetAll();
    }
}
