using System;
using Convertidor.Data.Entities.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhPeriodoService
	{

        Task<List<RH_PERIODOS>> GetAll();
        Task<List<RH_PERIODOS>> GetByTipoNomina(int tipoNomina);
        Task<List<RH_PERIODOS>> GetByYear(int ano);
    }
}

