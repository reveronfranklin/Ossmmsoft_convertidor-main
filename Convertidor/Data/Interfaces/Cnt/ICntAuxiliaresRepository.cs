using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntAuxiliaresRepository
    {
        Task<List<CNT_AUXILIARES>> GetAll();
    }
}
