using Convertidor.Data.EntitiesDestino;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services
{
    public interface IHistoricoPersonalCargoService
    {

        Task<ResultDto<HistoricoPersonalCargo>> TransferirHistoricoPersonalCargoPorCantidadDeDias(int dias);
        Task<List<RhTipoNominaCargosResponseDto>> GetListCargosPorPersona(int codigoPersona);



    }
}
