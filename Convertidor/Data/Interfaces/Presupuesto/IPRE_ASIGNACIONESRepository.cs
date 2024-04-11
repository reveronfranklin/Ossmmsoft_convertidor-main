using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPRE_ASIGNACIONESRepository
	{
		Task<PRE_ASIGNACIONES> GetByCodigo(int codigo);
		Task<List<PRE_ASIGNACIONES>> GetAll();
		Task<List<PRE_ASIGNACIONES>> GetAllByPresupuesto(int codigoPresupuesto);
		Task<List<PRE_ASIGNACIONES>> GetAllByIcp(int codigoPresupuesto, int codigoIcp);
		Task<List<PRE_ASIGNACIONES>> GetAllByIcpPuc(int codigoPresupuesto, int codigoIcp, int codigoPuc);
		Task<ResultDto<PRE_ASIGNACIONES>> Add(PRE_ASIGNACIONES entity);
		Task<ResultDto<PRE_ASIGNACIONES>> Update(PRE_ASIGNACIONES entity);
		Task<string> Delete(int codigoAsignacion);
		Task<int> GetNextKey();
		
        Task<bool> PresupuestoExiste(int codPresupuesto);
        Task<bool> PresupuestoExisteConMonto(int codPresupuesto);
        Task<bool> ICPExiste(int codigoICP);
        Task<bool> PUCExiste(int codigoPUC);
        Task<string> DeleteByPresupuesto(int codigoPresupuesto);
	}
}

