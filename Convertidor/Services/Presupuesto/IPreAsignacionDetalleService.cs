using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto;

public interface IPreAsignacionDetalleService
{
    Task<ResultDto<PreAsignacionesDetalleGetDto>> GetByCodigo(int codigo);
    Task<ResultDto<List<PreAsignacionesDetalleGetDto>>> GetAllByPresupuesto(PreAsignacionesDetalleFilterDto filterDto);
    Task<ResultDto<List<PreAsignacionesDetalleGetDto>>> GetAllByAsignacion(PreAsignacionesDetalleFilterDto filter);
    Task<ResultDto<PreAsignacionesDetalleGetDto>> Add(PreAsignacionesDetalleUpdateDto entity);
    Task<ResultDto<PreAsignacionesDetalleGetDto>> Update(PreAsignacionesDetalleUpdateDto entity);
    Task<ResultDto<string>> Delete(PreAsignacionesDetalleDeleteDto deleteDto);
    Task<bool> AsignacionExiste(int codigoAsignacion);
}