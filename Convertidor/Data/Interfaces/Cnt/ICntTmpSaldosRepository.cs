using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntTmpSaldosRepository
    {
        Task<List<CNT_TMP_SALDOS>> GetAll();
        Task<CNT_TMP_SALDOS> GetByCodigo(int codigoTmpSaldo); 
        Task<ResultDto<CNT_TMP_SALDOS>> Add(CNT_TMP_SALDOS entity);
        Task<ResultDto<CNT_TMP_SALDOS>> Update(CNT_TMP_SALDOS entity);
        Task<int> GetNextKey();
    }
}
