using System;
using Convertidor.Data.Entities.Rh;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhTipoNominaRepository
	{
        Task<List<RH_TIPOS_NOMINA>> GetAll();
        Task<List<RH_TIPOS_NOMINA>> GetTipoNominaByCodigoPersona(int codigoPersona, DateTime desde, DateTime hasta);
        Task<RH_TIPOS_NOMINA> GetByCodigo(int codigoTipoNomina);

    }
}

