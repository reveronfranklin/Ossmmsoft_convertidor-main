using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto;

public interface IPRE_SALDOSRepository
{
    Task<PRE_SALDOS> GetByCodigo(int codigo);
    Task<List<PRE_SALDOS>> GetAll();
    Task<List<PRE_SALDOS>> GetAllByPresupuesto(int codigoPresupuesto);
    Task<List<PRE_SALDOS>> GetAllByIcp(int codigoPresupuesto, int codigoIcp);
    Task<List<PRE_SALDOS>> GetAllByIcpPuc(int codigoPresupuesto, int codigoIcp, int codigoPuc);
    Task<ResultDto<PRE_SALDOS>> Add(PRE_SALDOS entity);
    Task<ResultDto<PRE_SALDOS>> Update(PRE_SALDOS entity);
    Task<string> Delete(int codigoAsignacion);
    Task<string> DeleteByPresupuesto(int codigoPresupuesto);
    Task<int> GetNextKey();
    Task<bool> PresupuestoExiste(int codPresupuesto);

    Task<bool> PresupuestoExisteConMonto(int codPresupuesto);
    Task<bool> ICPExiste(int codigoICP);
    Task<bool> PUCExiste(int codigoPUC);
    

}