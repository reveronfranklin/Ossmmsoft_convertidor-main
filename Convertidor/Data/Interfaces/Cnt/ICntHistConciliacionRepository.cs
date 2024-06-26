using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntHistConciliacionRepository
    {
        Task<List<CNT_HIST_CONCILIACION>> GetAll();
        Task<List<CNT_HIST_CONCILIACION>> GetByCodigoConciliacion(int codigoConciliacion);
        Task<ResultDto<CNT_HIST_CONCILIACION>> Add(CNT_HIST_CONCILIACION entity);
        Task<int> GetNextKey();
    }
}
