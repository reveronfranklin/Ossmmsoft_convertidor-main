using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntTmpConciliacionRepository
    {
        Task<List<CNT_TMP_CONCILIACION>> GetAll();
        Task<ResultDto<CNT_TMP_CONCILIACION>> Add(CNT_TMP_CONCILIACION entity);
        Task<int> GetNextKey();
    }
}
