using Convertidor.Data.Entities.Bm;

namespace Convertidor.Data.Interfaces.Bm;

public interface IBmConteoRepository
{
    Task<BM_CONTEO> GetByCodigo(int conteoId);
    Task<List<BM_CONTEO>> GetAll();
    Task<ResultDto<BM_CONTEO>> Add(BM_CONTEO entity);
    Task<ResultDto<BM_CONTEO>> Update(BM_CONTEO entity);
    Task<string> Delete(int tituloId);
    Task<int> GetNextKey();
    

}