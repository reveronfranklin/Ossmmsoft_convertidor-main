using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto;

public interface IPreAsignacionService
{
    Task<ResultDto<PreAsignacionesGetDto>> GetByCodigo(int codigo);
    Task<decimal> GetTotalAsignacionByIcpPuc(int codigoPresupuesto, int codigoIcp, int codigoPuc);
    Task<ResultDto<List<PreAsignacionesGetDto>>> GetAllByPresupuesto(PreAsignacionesFilterDto filterDto);
    Task<ResultDto<List<PreAsignacionesGetDto>>> GetAllByIcp(PreAsignacionesFilterDto filter);
    
    Task<ResultDto<List<PreAsignacionesGetDto>>> GetAllByIcpPuc(PreAsignacionesFilterDto filter);
    Task<ResultDto<PreAsignacionesGetDto>> Add(PreAsignacionesUpdateDto entity);
    Task<ResultDto<PreAsignacionesGetDto>> CreateListAsignaciones(PreAsignacionesExcel excel);
    Task<ResultDto<PreAsignacionesGetDto>> Update(PreAsignacionesUpdateDto entity);
    Task<ResultDto<string>> Delete(PreAsignacionesDeleteDto deleteDto);
    Task<bool> PresupuestoExiste(int codPresupuesto);
    Task<bool> ICPExiste(int codigoICP);
    Task<bool> PUCExiste(int codigoPUC);
    Task<ResultDto<PreAsignacionesGetDto>> UpdateField(UpdateFieldDto dto);
    Task<ResultDto<PreAsignacionesGetDto>> ValidarListAsignaciones(PreAsignacionesExcel excel);
    Task<ResultDto<string>> DeleteByPresupuesto(int codigoPresupuesto);
}