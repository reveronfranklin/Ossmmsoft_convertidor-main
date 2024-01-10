using Convertidor.Data.Entities.Rh;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhProcesoRepository
	{
        Task<RH_PROCESOS> GetByCodigo(int codigoProcesso);
        Task<List<RH_PROCESOS>> GetAll();

    }
}

