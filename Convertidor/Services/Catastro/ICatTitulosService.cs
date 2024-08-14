using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatTitulosService
    {
        Task<ResultDto<List<CatTitulosResponseDto>>> GetAll();
    }
}
