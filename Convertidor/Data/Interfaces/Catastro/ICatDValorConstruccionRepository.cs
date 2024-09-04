using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatDValorConstruccionRepository
    {
        Task<List<CAT_D_VALOR_CONSTRUCCION>> GetAll();
        Task<ResultDto<CAT_D_VALOR_CONSTRUCCION>> Add(CAT_D_VALOR_CONSTRUCCION entity);
        Task<int> GetNextKey();
    }
}
