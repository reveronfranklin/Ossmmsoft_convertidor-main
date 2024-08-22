using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatAvaluoConstruccionService
    {
        Task<ResultDto<List<CatAvaluoConstruccionResponseDto>>> GetAll();
        Task<ResultDto<CatAvaluoConstruccionResponseDto>> Create(CatAvaluoConstruccionUpdateDto dto);
    }
}
