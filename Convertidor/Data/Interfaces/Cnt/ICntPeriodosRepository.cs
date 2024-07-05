using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntPeriodosRepository
    {
        Task<List<CNT_PERIODOS>> GetAll();
    }
}
