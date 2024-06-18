using Convertidor.Data.Entities.Bm;

namespace Convertidor.Data.Interfaces.Bm;

public interface IBmConteoDetalleHistoricoRepository
{
    Task<List<BM_CONTEO_DETALLE_HISTORICO>> GetAllByConteo(int codigoConteo);
    Task<ResultDto<List<BM_CONTEO_DETALLE_HISTORICO>>> AddRange(List<BM_CONTEO_DETALLE_HISTORICO> entities);
    
}