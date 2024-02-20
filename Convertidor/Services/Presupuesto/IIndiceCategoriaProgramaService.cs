using Convertidor.Data.EntitiesDestino;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public interface IIndiceCategoriaProgramaService
    {
        Task<ResultDto<IndiceCategoriaPrograma>> TransferirIndiceCategoriaProgramaPorCantidadDeDias(int dias);
        Task<ResultDto<List<PreIndiceCategoriaProgramaticaGetDto>>> GetAllByCodigoPresupuesto(FilterByPresupuestoDto filter);
        Task<ResultDto<PreIndiceCategoriaProgramaticaGetDto>> Update(PreIndiceCategoriaProgramaticaUpdateDto dto);
        Task<ResultDto<PreIndiceCategoriaProgramaticaGetDto>> Create(PreIndiceCategoriaProgramaticaUpdateDto dto);
        Task<ResultDto<List<PreCodigosIcp>>> ListCodigosValidosIcp();
        Task<ResultDto<List<PreCodigosIcp>>> ListCodigosHistoricoIcp();
        Task<PreIndiceCategoriaProgramaticaGetDto> GetByCodigo(int codigoIcp);
        Task<ResultDto<DeletePreIcpDto>> Delete(DeletePreIcpDto dto);
        Task<ResultDto<List<PreIndiceCategoriaProgramaticaGetDto>>> UpdateIcpPadre(int codigoPresupuesto);
        Task<ResultDto<List<TreeICP>>> GetTreeByPresupuesto(int codigoPresupuesto);
        Task DeleteByCodigoPresupuesto(int codigoPresupuesto);
    }
}
