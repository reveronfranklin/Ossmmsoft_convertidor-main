using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPRE_V_DOC_MODIFICADORepository
	{


        Task<List<PRE_V_DOC_MODIFICADO>> GetByCodicoSaldo(FilterDocumentosPreVSaldo filter);

    }
}

