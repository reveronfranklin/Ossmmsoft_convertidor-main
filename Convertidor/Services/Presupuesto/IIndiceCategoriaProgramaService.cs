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
        Task<ResultDto<List<PreCodigosIcp>>> ListCodigosValidosIcp();
        Task<ResultDto<List<PreCodigosIcp>>> ListCodigosHistoricoIcp();
        Task<ResultDto<DeletePreIcpDto>> Delete(DeletePreIcpDto dto);
        Task<ResultDto<List<PreIndiceCategoriaProgramaticaGetDto>>> UpdateIcpPadre(int codigoPresupuesto);
    }
}
