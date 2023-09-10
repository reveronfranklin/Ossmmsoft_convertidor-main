using System;
using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhPersonaService
	{

        Task<List<PersonasDto>> GetAll();
        Task<ListPersonasDto> GetByCodigoPersona(int codigoPersona);
        Task<List<ListSimplePersonaDto>> GetAllSimple();
        Task<PersonasDto> GetPersona(int codigoPersona);
        Task<RH_RELACION_CARGOS> CargoActual(int codigoPersona);
	}
}

