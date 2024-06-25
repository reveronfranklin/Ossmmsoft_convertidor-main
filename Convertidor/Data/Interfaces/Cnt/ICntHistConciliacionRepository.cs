using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntHistConciliacionRepository
    {
        Task<List<CNT_HIST_CONCILIACION>> GetAll();
    }
}
