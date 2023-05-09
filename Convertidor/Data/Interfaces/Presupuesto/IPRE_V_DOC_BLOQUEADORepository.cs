using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPRE_V_DOC_BLOQUEADORepository
	{


        Task<List<PRE_V_DOC_BLOQUEADO>> GetByCodicoSaldo(FilterDocumentosPreVSaldo filter);

    }
}

