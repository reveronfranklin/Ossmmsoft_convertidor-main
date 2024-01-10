using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhTipoNominaRepository
	{
        Task<List<RH_TIPOS_NOMINA>> GetAll();
        Task<List<RH_TIPOS_NOMINA>> GetTipoNominaByCodigoPersona(int codigoPersona, DateTime desde, DateTime hasta);
        Task<RH_TIPOS_NOMINA> GetByCodigo(int codigoTipoNomina);
        Task<ResultDto<RH_TIPOS_NOMINA>> Add(RH_TIPOS_NOMINA entity);
        Task<ResultDto<RH_TIPOS_NOMINA>> Update(RH_TIPOS_NOMINA entity);
        Task<string> Delete(int codigoTipoNomina);
        Task<int> GetNextKey();

    }
}

