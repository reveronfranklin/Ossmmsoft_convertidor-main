using Convertidor.Dtos.Bm;
using Convertidor.Data.Entities.Bm;
namespace Convertidor.Data.Interfaces.Bm;

public interface IBmConteoDetalleHistoricoService
{
    Task<ResultDto<List<BmConteoDetalleResumenResponseDto>>> GetResumen(int codigoConteo);
    Task<ResultDto<List<BM_CONTEO_DETALLE_HISTORICO>>> AddRange(List<BM_CONTEO_DETALLE_HISTORICO> entities);
    Task<List<BM_CONTEO_DETALLE_HISTORICO>> GetAllByConteo(int codigoConteo);
}