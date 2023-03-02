

using Convertidor.Data.EntitiesDestino;

namespace Convertidor.Data.Interfaces
{
    public interface IHistoricoNominaRepository
    {
        Task<IEnumerable<HistoricoNomina>> Get();
        Task<List<HistoricoNomina>> GetByLastDay(int days);
        Task<List<HistoricoNomina>> GetByLastDayWithRelation(int days);
        Task<bool> Add(HistoricoNomina entity);
        Task<bool> Add(List<HistoricoNomina> entities);
        Task<HistoricoNomina> GetByKey(long key);
        Task Delete(long id);

        Task DeletePorDias(int days);
        Task<List<HistoricoNomina>> GetByPeriodo(int codigoPeriodo, int tipoNomina);
    }
}
