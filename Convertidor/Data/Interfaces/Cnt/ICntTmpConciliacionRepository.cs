using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntTmpConciliacionRepository
    {
        Task<List<CNT_TMP_CONCILIACION>> GetAll();
    }
}
