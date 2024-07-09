using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntTmpAnaliticoRepository
    {
        Task<List<CNT_TMP_ANALITICO>> GetAll();
    }
}
