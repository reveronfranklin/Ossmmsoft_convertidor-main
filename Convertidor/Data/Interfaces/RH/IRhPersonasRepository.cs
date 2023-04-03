using System;
using Convertidor.Data.Entities.Rh;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhPersonasRepository
	{
        Task<List<RH_PERSONAS>> GetAll();
        Task<RH_PERSONAS> GetCedula(int cedula);
        Task<RH_PERSONAS> GetCodogoPersona(int codigoPersona);

    }
}

