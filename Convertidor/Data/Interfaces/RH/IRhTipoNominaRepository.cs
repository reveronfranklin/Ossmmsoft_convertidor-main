using System;
using Convertidor.Data.Entities.Rh;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhTipoNominaRepository
	{
        Task<List<RH_TIPOS_NOMINA>> GetAll();


    }
}

