using Convertidor.Data.EntitiesDestino;

namespace Convertidor.Data.Interfaces
{
    public interface IHistoricoRetencionesRepository
    {

        Task<bool> Add(HistoricoRetenciones entity);
        Task<bool> Add(List<HistoricoRetenciones> entities);
        Task<List<HistoricoRetenciones>> GetByLastDay(int days);
        Task<HistoricoRetenciones> GetByKey(long key);
        Task Delete(long id);
        Task DeletePorDias(int days);

    }
}
