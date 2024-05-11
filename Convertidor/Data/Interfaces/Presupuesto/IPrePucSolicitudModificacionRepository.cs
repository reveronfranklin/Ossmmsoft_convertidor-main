using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto;

public interface IPrePucSolicitudModificacionRepository
{
    Task<PRE_PUC_SOL_MODIFICACION> GetByCodigo(int codigo);
    Task<List<PRE_PUC_SOL_MODIFICACION>> GetAll();
    Task<List<PRE_PUC_SOL_MODIFICACION>> GetAllByPresupuesto(int codigoPresupuesto);
    Task<List<PRE_PUC_SOL_MODIFICACION>> GetAllByIcp(int codigoPresupuesto, int codigoIcp);
    Task<List<PRE_PUC_SOL_MODIFICACION>> GetAllByIcpPuc(int codigoPresupuesto, int codigoIcp, int codigoPuc);
    Task<ResultDto<PRE_PUC_SOL_MODIFICACION>> Add(PRE_PUC_SOL_MODIFICACION entity);
    Task<ResultDto<PRE_PUC_SOL_MODIFICACION>> Update(PRE_PUC_SOL_MODIFICACION entity);
    Task<string> Delete(int codigo);
    Task<int> GetNextKey();
    Task<bool> PresupuestoExiste(int codPresupuesto);
    Task<bool> ICPExiste(int codigoICP);
    Task<bool> PUCExiste(int codigoPUC);
    Task<List<PRE_PUC_SOL_MODIFICACION>> GetAllByCodigoSolicitud(int codigoSolicitud);
    Task<PRE_PUC_SOL_MODIFICACION> GetByCodigoSolModificacionCodigoSaldo(int codigoSolModificacion, int codigoSaldo);


}