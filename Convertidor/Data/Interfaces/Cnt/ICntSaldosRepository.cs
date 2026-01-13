using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntSaldosRepository
    {
        Task<List<CNT_SALDOS>> GetAll();
        Task<CNT_SALDOS> GetByCodigo(int codigoSaldo);
        Task<ResultDto<CNT_SALDOS>> Add(CNT_SALDOS entity);
        Task<ResultDto<CNT_SALDOS>> Update(CNT_SALDOS entity);
        Task<string> Delete(int codigoSaldo);
        Task<int> GetNextKey();
    }
}
