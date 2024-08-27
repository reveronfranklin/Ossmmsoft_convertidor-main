using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatControlParcelasService
    {
        Task<ResultDto<List<CatControlParcelasResponseDto>>> GetAll();
    }
}
