using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatAforosInmueblesRepository
    {
        Task<List<CAT_AFOROS_INMUEBLES>> GetAll();
        Task<ResultDto<CAT_AFOROS_INMUEBLES>> Add(CAT_AFOROS_INMUEBLES entity);
        Task<int> GetNextKey();
    }
}
