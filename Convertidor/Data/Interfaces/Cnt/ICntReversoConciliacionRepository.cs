using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntReversoConciliacionRepository
    {
        Task<List<CNT_REVERSO_CONCILIACION>> GetAll();
        Task<CNT_REVERSO_CONCILIACION> GetByCodigo(int codigoHistConciliacion);
        Task<List<CNT_REVERSO_CONCILIACION>> GetByCodigoConciliacion(int codigoConciliacion);
        Task<ResultDto<CNT_REVERSO_CONCILIACION>> Add(CNT_REVERSO_CONCILIACION entity);
        Task<ResultDto<CNT_REVERSO_CONCILIACION>> Update(CNT_REVERSO_CONCILIACION entity);
        Task<int> GetNextKey();
    }
}
