using System;
using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis
{
	public interface ISisUbicacionNacionalRepository
	{
        Task<List<SIS_USUARIOS>> GetALL();


    }
}

