using Convertidor.Data.EntitiesDestino;
using Convertidor.Dtos;

namespace Convertidor.Services
{
    public interface IHistoricoRetencionesService
    {

        Task<ResultDto<HistoricoRetenciones>> GeneraHistoricoRetencionesPorCantidadDeDias(int dias);
    }
}
