using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto;

public interface IPreAsignacionesDetalleRepository
{
    Task<PRE_ASIGNACIONES_DETALLE> GetByCodigo(int codigo);
    Task<List<PRE_ASIGNACIONES_DETALLE>> GetAll();
    Task<List<PRE_ASIGNACIONES_DETALLE>> GetAllByPresupuesto(int codigoPresupuesto);
    Task<List<PRE_ASIGNACIONES_DETALLE>> GetAllByAsignacion(int codigoAsignacion);
    Task<ResultDto<PRE_ASIGNACIONES_DETALLE>> Add(PRE_ASIGNACIONES_DETALLE entity);
    Task<ResultDto<PRE_ASIGNACIONES_DETALLE>> Update(PRE_ASIGNACIONES_DETALLE entity);
    Task<string> Delete(int codigoAsignacionDetalle);
    Task<int> GetNextKey();
    Task<bool> AsignacionExiste(int codigoAsignacion);
}