using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPRE_V_DOC_PAGADORepository
	{



        Task<List<PRE_V_DOC_PAGADO>> GetByCodicoSaldo(FilterDocumentosPreVSaldo filter);

    }
}

