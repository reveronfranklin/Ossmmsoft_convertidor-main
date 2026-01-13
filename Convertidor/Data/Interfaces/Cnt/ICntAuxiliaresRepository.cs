using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntAuxiliaresRepository
    {
        Task<List<CNT_AUXILIARES>> GetAll();
        Task<CNT_AUXILIARES> GetByCodigo(int codigoAuxiliar);
        Task<List<CNT_AUXILIARES>> GetByCodigoMayor(int codigoMayor);
        Task<ResultDto<CNT_AUXILIARES>> Add(CNT_AUXILIARES entity);
        Task<ResultDto<CNT_AUXILIARES>> Update(CNT_AUXILIARES entity);
        Task<string> Delete(int codigoAuxiliar);
        Task<int> GetNextKey();
    }
}
