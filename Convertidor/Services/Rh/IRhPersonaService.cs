using System;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhPersonaService
	{

        Task<List<PersonasDto>> GetAll();
        Task<ListPersonasDto> GetByCodigoPersona(int codigoPersona);

    }
}

