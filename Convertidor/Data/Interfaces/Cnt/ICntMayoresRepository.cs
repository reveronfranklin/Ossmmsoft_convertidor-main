using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntMayoresRepository
    {
        Task<List<CNT_MAYORES>> GetAll();
    }
}
