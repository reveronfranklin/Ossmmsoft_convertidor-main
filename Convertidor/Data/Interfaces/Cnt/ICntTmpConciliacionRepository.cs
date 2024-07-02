using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntTmpConciliacionRepository
    {
        Task<List<CNT_TMP_CONCILIACION>> GetAll();
        Task<CNT_TMP_CONCILIACION> GetByCodigo(int codigoTmpConciliacion);
        Task<ResultDto<CNT_TMP_CONCILIACION>> Add(CNT_TMP_CONCILIACION entity);
        Task<ResultDto<CNT_TMP_CONCILIACION>> Update(CNT_TMP_CONCILIACION entity);
        Task<int> GetNextKey();
    }
}
