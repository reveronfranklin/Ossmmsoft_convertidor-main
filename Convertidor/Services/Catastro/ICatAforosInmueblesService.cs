using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatAforosInmueblesService
    {
        Task<ResultDto<List<CatAforosInmueblesResponseDto>>> GetAll();
    }
}
