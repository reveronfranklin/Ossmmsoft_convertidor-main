using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatAforosInmueblesRepository
    {
        Task<List<CAT_AFOROS_INMUEBLES>> GetAll();
        Task<CAT_AFOROS_INMUEBLES> GetByCodigo(int codigoAforoInmueble);
        Task<ResultDto<CAT_AFOROS_INMUEBLES>> Add(CAT_AFOROS_INMUEBLES entity);
        Task<ResultDto<CAT_AFOROS_INMUEBLES>> Update(CAT_AFOROS_INMUEBLES entity);
        Task<int> GetNextKey();
    }
}
