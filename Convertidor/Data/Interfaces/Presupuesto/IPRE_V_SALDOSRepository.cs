using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPRE_V_SALDOSRepository
	{
        Task<IEnumerable<PRE_V_SALDOS>> GetAll(FilterPRE_V_SALDOSDto filter);
        
    }
}

