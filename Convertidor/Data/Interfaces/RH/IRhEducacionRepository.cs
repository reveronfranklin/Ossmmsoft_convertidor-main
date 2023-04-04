using System;
using Convertidor.Data.Entities.Rh;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhEducacionRepository
	{
        Task<List<RH_EDUCACION>> GetAll();
        Task<List<RH_EDUCACION>> GetByCodigoPersona(int codigoPersona);

    }
}

