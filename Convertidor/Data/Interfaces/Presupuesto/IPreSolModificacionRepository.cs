using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPreSolModificacionRepository
    {


        Task<PRE_SOL_MODIFICACION> GetByCodigo(int CodigoSolModificacion);
        Task<List<PRE_SOL_MODIFICACION>> GetAll();
        Task<ResultDto<PRE_SOL_MODIFICACION>> Add(PRE_SOL_MODIFICACION entity);
        Task<ResultDto<PRE_SOL_MODIFICACION>> Update(PRE_SOL_MODIFICACION entity);
        Task<string> Delete(int CodigoSolModificacion);
        Task<int> GetNextKey();
        Task<List<PRE_SOL_MODIFICACION>> GetByPresupuesto(int codigoPresupuesto);
    }
}

