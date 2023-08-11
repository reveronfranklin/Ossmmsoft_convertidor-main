using System;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhTipoNominaService
	{
        Task<List<ListTipoNominaDto>> GetAll();

        Task<List<ListTipoNominaDto>> GetTipoNominaByCodigoPersona(int codigoPersona, DateTime desde, DateTime hasta);

    }
}

