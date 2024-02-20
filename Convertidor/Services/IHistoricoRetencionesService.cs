using Convertidor.Data.EntitiesDestino;

namespace Convertidor.Services
{
    public interface IHistoricoRetencionesService
    {

        Task<ResultDto<HistoricoRetenciones>> GeneraHistoricoRetencionesPorCantidadDeDias(int dias);
    }
}
