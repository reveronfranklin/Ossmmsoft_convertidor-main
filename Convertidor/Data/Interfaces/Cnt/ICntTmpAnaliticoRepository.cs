using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntTmpAnaliticoRepository
    {
        Task<List<CNT_TMP_ANALITICO>> GetAll();
        Task<CNT_TMP_ANALITICO> GetByCodigo(int codigoTmpAnalitico);
        Task<ResultDto<CNT_TMP_ANALITICO>> Add(CNT_TMP_ANALITICO entity);
        Task<ResultDto<CNT_TMP_ANALITICO>> Update(CNT_TMP_ANALITICO entity);
        Task<string> Delete(int codigoTmpAnalitico);
       
        Task<int> GetNextKey();
    }
}
