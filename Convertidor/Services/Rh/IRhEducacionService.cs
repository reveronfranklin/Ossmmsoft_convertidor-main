using System;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhEducacionService
	{
        Task<List<ListEducacionDto>> GetByCodigoPersona(int codigoPersona);


    }
}

