using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntAuxiliaresPucRepository
    {
        Task<List<CNT_AUXILIARES_PUC>> GetAll();
        Task<ResultDto<CNT_AUXILIARES_PUC>> Add(CNT_AUXILIARES_PUC entity);
        Task<int> GetNextKey();
    }
}
