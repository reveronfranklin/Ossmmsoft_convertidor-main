using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntBalancesRepository
    {
        Task<List<CNT_BALANCES>> GetAll();
        Task<CNT_BALANCES> GetByCodigo(int codigoBalance);
        Task<ResultDto<CNT_BALANCES>> Add(CNT_BALANCES entity);
        Task<ResultDto<CNT_BALANCES>> Update(CNT_BALANCES entity);
        Task<string> Delete(int codigoBalance);
        Task<int> GetNextKey();
    }
}
