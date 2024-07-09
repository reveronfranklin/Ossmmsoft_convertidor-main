using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntTmpSaldosRepository
    {
        Task<List<CNT_TMP_SALDOS>> GetAll();
    }
}
