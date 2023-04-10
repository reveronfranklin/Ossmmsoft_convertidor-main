using System;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhConceptosService
	{

        Task<List<ListConceptosDto>> GetAll();


    }
}

