namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhHPeriodoRepository
	{

        Task<List<RH_H_PERIODOS>> GetAll(PeriodoFilterDto filter);

        Task<List<RH_H_PERIODOS>> GetByTipoNomina(int tipoNomina);
        Task<List<RH_H_PERIODOS>> GetByYear(int ano);
        Task<RH_H_PERIODOS> GetByCodigo(int codigoPeriodo);
        Task<ResultDto<RH_H_PERIODOS>> Add(RH_H_PERIODOS entity);
        Task<ResultDto<RH_H_PERIODOS>> Update(RH_H_PERIODOS entity);
        Task<string> Delete(int codigoPeriodo);
        Task<int> GetNextKey();
    }
}

