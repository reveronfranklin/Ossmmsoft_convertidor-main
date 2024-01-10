using Convertidor.Data.Entities.Rh;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhProcesoDetalleRepository
	{

        Task<RH_PROCESOS_DETALLES> GetByCodigo(int codigoProcesoDetalle);
        Task<List<RH_PROCESOS_DETALLES>> GetByCodigoProceso(int codigoProceso);
        Task<List<RH_PROCESOS_DETALLES>> GetAll();
    }
}

