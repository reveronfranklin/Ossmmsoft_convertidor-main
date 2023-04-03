using System;
using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos.Rh;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhPeriodoRepository
	{

        Task<List<RH_PERIODOS>> GetAll(PeriodoFilterDto filter);

        Task<List<RH_PERIODOS>> GetByTipoNomina(int tipoNomina);
        Task<List<RH_PERIODOS>> GetByYear(int ano);
    }
}

