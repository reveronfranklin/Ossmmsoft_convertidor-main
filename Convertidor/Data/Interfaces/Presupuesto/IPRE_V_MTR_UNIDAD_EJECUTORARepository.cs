using System;
using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPRE_V_MTR_UNIDAD_EJECUTORARepository
	{

        Task<List<PRE_V_MTR_UNIDAD_EJECUTORA>> GetAll();


    }
}

