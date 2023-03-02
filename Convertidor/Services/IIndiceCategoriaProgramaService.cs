using Convertidor.Data.EntitiesDestino;
using Convertidor.Dtos;

namespace Convertidor.Services
{
    public interface IIndiceCategoriaProgramaService
    {
        Task<ResultDto<IndiceCategoriaPrograma>> TransferirIndiceCategoriaProgramaPorCantidadDeDias(int dias);
    }
}
