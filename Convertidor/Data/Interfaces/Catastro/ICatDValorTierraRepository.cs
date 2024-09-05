using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatDValorTierraRepository
    {
        Task<List<CAT_D_VALOR_TIERRA>> GetAll();
    }
}
