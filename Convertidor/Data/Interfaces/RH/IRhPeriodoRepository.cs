using System;
using Convertidor.Data.Entities.Rh;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhPeriodoRepository
	{


        Task<List<RH_PERIODOS>> GetAll();
        Task<List<RH_PERIODOS>> GetByTipoNomina(int tipoNomina);
    }
}

