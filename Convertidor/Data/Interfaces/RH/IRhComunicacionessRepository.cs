using System;
using Convertidor.Data.Entities.Rh;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhComunicacionessRepository
	{
        Task<List<RH_COMUNICACIONES>> GetByCodigoPersona(int codigoPersona);


    }
}

