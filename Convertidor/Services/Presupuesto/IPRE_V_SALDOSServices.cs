using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPRE_V_SALDOSServices
	{

       
        Task<List<PreVSaldosGetDto>> GetAll(FilterPRE_V_SALDOSDto filter);

    }
}

