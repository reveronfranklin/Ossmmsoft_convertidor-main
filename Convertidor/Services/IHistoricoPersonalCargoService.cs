using Convertidor.Data.EntitiesDestino;

namespace Convertidor.Services
{
    public interface IHistoricoPersonalCargoService
    {

        Task<ResultDto<HistoricoPersonalCargo>> TransferirHistoricoPersonalCargoPorCantidadDeDias(int dias);
        Task<List<RhTipoNominaCargosResponseDto>> GetListCargosPorPersona(int codigoPersona);



    }
}
