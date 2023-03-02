using Convertidor.Data.EntitiesDestino;
using Convertidor.Dtos;

namespace Convertidor.Services
{
    public interface IHistoricoPersonalCargoService
    {

        Task<ResultDto<HistoricoPersonalCargo>> TransferirHistoricoPersonalCargoPorCantidadDeDias(int dias);

    }
}
