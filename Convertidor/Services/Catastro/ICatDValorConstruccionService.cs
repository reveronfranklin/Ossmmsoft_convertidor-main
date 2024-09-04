using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatDValorConstruccionService
    {
        Task<ResultDto<List<CatDValorContruccionResponseDto>>> GetAll();
    }
}
