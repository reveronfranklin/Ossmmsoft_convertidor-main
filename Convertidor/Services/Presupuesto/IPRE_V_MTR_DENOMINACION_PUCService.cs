using System;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPRE_V_MTR_DENOMINACION_PUCService
	{

        Task<List<ListPreMtrDenominacionPuc>> GetAll();

    }
}

