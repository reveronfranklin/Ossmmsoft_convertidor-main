using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhProcesoRepository
	{
        Task<RH_PROCESOS> GetByCodigo(int codigoProcesso);
        Task<List<RH_PROCESOS>> GetAll();
        Task<ResultDto<RH_PROCESOS>> Add(RH_PROCESOS entity);
        Task<ResultDto<RH_PROCESOS>> Update(RH_PROCESOS entity);
        Task<string> Delete(int codigoAdministrativo);
        Task<int> GetNextKey();
        

	}
}

