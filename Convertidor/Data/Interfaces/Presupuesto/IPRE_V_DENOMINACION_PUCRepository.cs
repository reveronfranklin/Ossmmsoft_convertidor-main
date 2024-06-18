using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPRE_V_DENOMINACION_PUCRepository
	{


        Task<IEnumerable<PRE_V_DENOMINACION_PUC>> GetAll(FilterPRE_V_DENOMINACION_PUC filter);
        Task<IEnumerable<PRE_V_DENOMINACION_PUC>> GetByCodigoPresupuesto(int codigoPresupuesto);
    }
}

