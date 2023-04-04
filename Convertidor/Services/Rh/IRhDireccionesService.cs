using System;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhDireccionesService
	{

        Task<List<ListDireccionesDto>> GetByCodigoPersona(int codigoPersona);

    }
}

