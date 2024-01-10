using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhMovNominaRepository
	{
        Task<RH_MOV_NOMINA> GetByCodigo(int codigoMovNomina);
        Task<List<RH_MOV_NOMINA>> GetAll();
        Task<ResultDto<RH_MOV_NOMINA>> Add(RH_MOV_NOMINA entity);
        Task<ResultDto<RH_MOV_NOMINA>> Update(RH_MOV_NOMINA entity);
        Task<string> Delete(int codigoMovNomina);
        Task<int> GetNextKey();
        Task<RH_MOV_NOMINA> GetByTipoNominaPersonaConcepto(int codigoTipoNomina, int codigoPersona, int codigoConcepto);

    }
}

