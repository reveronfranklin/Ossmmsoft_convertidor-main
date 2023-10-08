using System;
using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis
{
	public interface 
		ISisUbicacionNacionalRepository
	{
        Task<SIS_UBICACION_NACIONAL> GetPais(int pais);
        Task<List<SIS_UBICACION_NACIONAL>> GetPaises();
        Task<List<SIS_UBICACION_NACIONAL>> GetEstados();
        Task<SIS_UBICACION_NACIONAL> GetEstado(int pais, int estado);

	}
}

