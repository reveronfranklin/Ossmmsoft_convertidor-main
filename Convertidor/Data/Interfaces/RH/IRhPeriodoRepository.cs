using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhPeriodoRepository
	{

        Task<List<RH_PERIODOS>> GetAll(PeriodoFilterDto filter);

        Task<List<RH_PERIODOS>> GetByTipoNomina(int tipoNomina);
        Task<List<RH_PERIODOS>> GetByYear(int ano);
        Task<RH_PERIODOS> GetByCodigo(int codigoPeriodo);
        Task<ResultDto<RH_PERIODOS>> Add(RH_PERIODOS entity);
        Task<ResultDto<RH_PERIODOS>> Update(RH_PERIODOS entity);
        Task<string> Delete(int codigoPeriodo);
        Task<int> GetNextKey();
    }
}

