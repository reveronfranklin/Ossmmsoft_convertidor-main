using Convertidor.Data.Entities.Bm;

namespace Convertidor.Data.Interfaces.Bm;

public interface IBmConteoHistoricoRepository
{
    Task<BM_CONTEO_HISTORICO> GetByCodigo(int conteoId);
    Task<List<BM_CONTEO_HISTORICO>> GetAll();
    Task<ResultDto<bool>> Add(BM_CONTEO_HISTORICO entities);
}