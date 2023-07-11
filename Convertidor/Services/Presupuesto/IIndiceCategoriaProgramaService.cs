using Convertidor.Data.EntitiesDestino;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public interface IIndiceCategoriaProgramaService
    {
        Task<ResultDto<IndiceCategoriaPrograma>> TransferirIndiceCategoriaProgramaPorCantidadDeDias(int dias);
        Task<ResultDto<List<PreIndiceCategoriaProgramaticaGetDto>>> GetAllByCodigoPresupuesto(int codigoPresupuesto);
        Task<ResultDto<PreIndiceCategoriaProgramaticaGetDto>> Update(PreIndiceCategoriaProgramaticaUpdateDto dto);
        Task<ResultDto<PreIndiceCategoriaProgramaticaGetDto>> Create(PreIndiceCategoriaProgramaticaUpdateDto dto);
    }
}
