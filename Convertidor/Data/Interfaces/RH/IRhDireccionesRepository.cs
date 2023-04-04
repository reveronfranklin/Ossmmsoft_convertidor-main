using System;
using Convertidor.Data.Entities.Rh;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhDireccionesRepository
	{

        Task<List<RH_DIRECCIONES>> GetByCodigoPersona(int codigoPersona);

    }
}

