using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntSaldosRepository
    {
        Task<List<CNT_SALDOS>> GetAll();
    }
}
