using Convertidor.Data.EntitiesDestino;

namespace Convertidor.Data.Interfaces
{
    public interface IIndiceCategoriaProgramaRepository
    {
        Task<bool> Add(IndiceCategoriaPrograma entity);
        Task<bool> Add(List<IndiceCategoriaPrograma> entities);
        Task<List<IndiceCategoriaPrograma>> GetByLastDay(int days);
        Task<IndiceCategoriaPrograma> GetByKey(long key);
        Task<List<IndiceCategoriaPrograma>> GetAll();
        Task Delete(long id);

        Task DeletePorDias(int days);
        Task DeleteAll();

    }
}
