using System;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPRE_V_MTR_UNIDAD_EJECUTORAService
    {

        Task<List<ListPreMtrUnidadEjecutora>> GetAll();
        Task<List<ListPreMtrUnidadEjecutora>> GetAllByPresupuesto(int codigoPresupuesto);

    }
}

