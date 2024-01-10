using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICAT_FICHARepository
    {
        Task<List<CAT_FICHA>> Get();
        Task<bool> Add(CAT_FICHA entity);
        Task Update(CAT_FICHA entity);
        Task<int> GetNext();
        Task<CAT_FICHA> GetByKey(int codigoFicha);
        Task Delete(int id);
    }
}
