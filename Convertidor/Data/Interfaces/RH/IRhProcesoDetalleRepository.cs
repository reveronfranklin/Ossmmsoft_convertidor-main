namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhProcesoDetalleRepository
	{

        Task<RH_PROCESOS_DETALLES> GetByCodigo(int codigoProcesoDetalle);
        Task<List<RH_PROCESOS_DETALLES>> GetByCodigoProceso(int codigoProceso);
        Task<List<RH_PROCESOS_DETALLES>> GetAll();
        Task<ResultDto<RH_PROCESOS_DETALLES>> Add(RH_PROCESOS_DETALLES entity);
        Task<ResultDto<RH_PROCESOS_DETALLES>> Update(RH_PROCESOS_DETALLES entity);
        Task<string> Delete(int codigoAdministrativo);
        Task<int> GetNextKey();
        Task<List<RH_PROCESOS_DETALLES>> GetByCodigoProcesoTipoNomina(int codigoProceso, int codigoTipoNomina);

	}
}

