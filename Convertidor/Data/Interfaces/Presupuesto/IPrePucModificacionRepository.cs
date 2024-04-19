using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPrePucModificacionRepository
    {
        Task<PRE_PUC_MODIFICACION> GetByCodigo(int codigoPucModificacion);
        Task<List<PRE_PUC_MODIFICACION>> GetAll();
        Task<ResultDto<PRE_PUC_MODIFICACION>> Add(PRE_PUC_MODIFICACION entity);
        Task<ResultDto<PRE_PUC_MODIFICACION>> Update(PRE_PUC_MODIFICACION entity);
        Task<string> Delete(int codigoPucModificacion);
        Task<int> GetNextKey();
    }
}

