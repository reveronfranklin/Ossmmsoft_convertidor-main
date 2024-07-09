using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntTmpHistAnaliticoRepository
    {
        Task<List<CNT_TMP_HIST_ANALITICO>> GetAll();
        Task<ResultDto<CNT_TMP_HIST_ANALITICO>> Add(CNT_TMP_HIST_ANALITICO entity);
        Task<int> GetNextKey();
    }
}
