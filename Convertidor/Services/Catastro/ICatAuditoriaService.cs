using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatAuditoriaService
    {
        Task<ResultDto<List<CatAuditoriaResponseDto>>> GetAll();
    }
}
