using Convertidor.Data.Entities.Bm;

namespace Convertidor.Data.Interfaces.Bm;

public interface IBmPlacasCuarentenaRepository
{
    Task<BM_PLACAS_CUARENTENA> GetByNumeroPlaca(string numeroPlaca);
    Task<BM_PLACAS_CUARENTENA> GetByCodigo(int codigoPlacaCuarentena);
    Task<List<BM_PLACAS_CUARENTENA>> GetAll();
    Task<ResultDto<BM_PLACAS_CUARENTENA>> Add(BM_PLACAS_CUARENTENA entity);
    Task<ResultDto<BM_PLACAS_CUARENTENA>> Update(BM_PLACAS_CUARENTENA entity);
    Task<string> Delete(int codigoPlacaCuarentena);
    Task<int> GetNextKey();
    
}