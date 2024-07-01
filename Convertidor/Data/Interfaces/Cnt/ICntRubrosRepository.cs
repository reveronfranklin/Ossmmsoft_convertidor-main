using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntRubrosRepository
    {
        Task<List<CNT_RUBROS>> GetAll();
    }
}
