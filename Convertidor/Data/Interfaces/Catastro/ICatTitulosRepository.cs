using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatTitulosRepository
    {
        Task<List<CAT_TITULOS>> GetAll();
    }
}
