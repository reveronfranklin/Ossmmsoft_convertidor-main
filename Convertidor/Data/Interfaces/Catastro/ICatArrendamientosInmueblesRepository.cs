using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatArrendamientosInmueblesRepository
    {
        Task<List<CAT_ARRENDAMIENTOS_INMUEBLES>> GetAll();
        Task<ResultDto<CAT_ARRENDAMIENTOS_INMUEBLES>> Add(CAT_ARRENDAMIENTOS_INMUEBLES entity);
        Task<CAT_ARRENDAMIENTOS_INMUEBLES> GetByCodigo(int codigoArrendamientoInmueble);
        Task<ResultDto<CAT_ARRENDAMIENTOS_INMUEBLES>> Update(CAT_ARRENDAMIENTOS_INMUEBLES entity);
        Task<int> GetNextKey();
    }
}
