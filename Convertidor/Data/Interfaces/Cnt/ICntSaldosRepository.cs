using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntSaldosRepository
    {
        Task<List<CNT_SALDOS>> GetAll();
        Task<ResultDto<CNT_SALDOS>> Add(CNT_SALDOS entity);
        Task<int> GetNextKey();
    }
}
