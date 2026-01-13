using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntHistAnaliticoRepository
    {
        Task<List<CNT_HIST_ANALITICO>> GetAll();
        Task<CNT_HIST_ANALITICO> GetByCodigo(int codigoHistAnalitico);
        Task<ResultDto<CNT_HIST_ANALITICO>> Add(CNT_HIST_ANALITICO entity);
        Task<ResultDto<CNT_HIST_ANALITICO>> Update(CNT_HIST_ANALITICO entity);
        Task<string> Delete(int codigoHistAnalitico);
        Task<int> GetNextKey();
    }
}
