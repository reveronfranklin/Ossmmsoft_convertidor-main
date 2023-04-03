using System;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhTipoNominaService
	{
        Task<List<ListTipoNominaDto>> GetAll();


    }
}

