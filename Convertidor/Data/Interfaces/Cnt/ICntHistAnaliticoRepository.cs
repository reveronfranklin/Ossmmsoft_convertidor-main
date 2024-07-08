using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntHistAnaliticoRepository
    {
        Task<List<CNT_HIST_ANALITICO>> GetAll();
    }
}
