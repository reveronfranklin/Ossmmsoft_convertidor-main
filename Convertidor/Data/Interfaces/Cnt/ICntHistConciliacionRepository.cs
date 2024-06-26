using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntHistConciliacionRepository
    {
        Task<List<CNT_HIST_CONCILIACION>> GetAll();
        Task<List<CNT_HIST_CONCILIACION>> GetByCodigoConciliacion(int codigoConciliacion);
        Task<CNT_HIST_CONCILIACION> GetByCodigo(int codigoHistConciliacion);
        Task<ResultDto<CNT_HIST_CONCILIACION>> Add(CNT_HIST_CONCILIACION entity);
        Task<ResultDto<CNT_HIST_CONCILIACION>> Update(CNT_HIST_CONCILIACION entity);
        Task<int> GetNextKey();
    }
}
