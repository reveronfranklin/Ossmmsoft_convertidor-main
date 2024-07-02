using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntBalancesRepository
    {
        Task<List<CNT_BALANCES>> GetAll();
    }
}
