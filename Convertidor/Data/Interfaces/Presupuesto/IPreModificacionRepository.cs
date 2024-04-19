using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPreModificacionRepository
    {
        Task<PRE_MODIFICACION> GetByCodigo(int codigoModificacion);
        Task<List<PRE_MODIFICACION>> GetAll();
        Task<ResultDto<PRE_MODIFICACION>> Add(PRE_MODIFICACION entity);
        Task<ResultDto<PRE_MODIFICACION>> Update(PRE_MODIFICACION entity);
        Task<string> Delete(int codigoModificacion);
        Task<int> GetNextKey();
    }
}

