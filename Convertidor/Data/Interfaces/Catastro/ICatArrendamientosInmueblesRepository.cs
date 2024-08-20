using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatArrendamientosInmueblesRepository
    {
        Task<List<CAT_ARRENDAMIENTOS_INMUEBLES>> GetAll();
    }
}
