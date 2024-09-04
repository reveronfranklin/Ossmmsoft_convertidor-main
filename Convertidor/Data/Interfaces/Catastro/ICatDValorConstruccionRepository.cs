using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatDValorConstruccionRepository
    {
        Task<List<CAT_D_VALOR_CONSTRUCCION>> GetAll();
    }
}
