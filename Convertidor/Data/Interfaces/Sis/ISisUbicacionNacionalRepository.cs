using System;
using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis
{
	public interface ISisUbicacionNacionalRepository
	{
        Task<SIS_UBICACION_NACIONAL> GetPais(int pais);


    }
}

