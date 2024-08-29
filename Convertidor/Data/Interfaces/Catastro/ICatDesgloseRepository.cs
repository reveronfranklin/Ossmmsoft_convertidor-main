using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatDesgloseRepository
    {
        Task<List<CAT_DESGLOSE>> GetAll();
    }
}
