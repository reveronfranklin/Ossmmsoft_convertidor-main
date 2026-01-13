using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ITmpAuxiliaresRepository
    {
        Task<List<TMP_AUXILIARES>> GetAll();
        Task<TMP_AUXILIARES> GetByCodigo(int codigoAuxiliar);
        Task<ResultDto<TMP_AUXILIARES>> Add(TMP_AUXILIARES entity);
        Task<ResultDto<TMP_AUXILIARES>> Update(TMP_AUXILIARES entity);
        Task<string> Delete(int codigoAuxiliar);
        Task<int> GetNextKey();
    }
}
