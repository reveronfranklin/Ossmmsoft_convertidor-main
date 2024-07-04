using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntAuxiliaresPucRepository
    {
        Task<List<CNT_AUXILIARES_PUC>> GetAll();
    }
}
