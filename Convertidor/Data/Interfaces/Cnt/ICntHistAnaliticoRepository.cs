using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntHistAnaliticoRepository
    {
        Task<List<CNT_HIST_ANALITICO>> GetAll();
        Task<ResultDto<CNT_HIST_ANALITICO>> Add(CNT_HIST_ANALITICO entity);
        Task<int> GetNextKey();
    }
}
