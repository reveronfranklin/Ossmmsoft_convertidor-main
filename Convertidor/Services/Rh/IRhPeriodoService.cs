using System;
using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhPeriodoService
	{


        Task<List<RH_PERIODOS>> GetAll(PeriodoFilterDto filter);
        Task<List<RH_PERIODOS>> GetByTipoNomina(int tipoNomina);
        Task<List<ListPeriodoDto>> GetByYear(int ano);
    }
}

