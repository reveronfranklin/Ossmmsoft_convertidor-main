using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhExpLaboralRepository
    {
        Task<RH_EXP_LABORAL> GetByCodigo(int codigoExpLaboral);
        Task<List<RH_EXP_LABORAL>> GetByCodigoPersona(int CodigoPersona);
        Task<ResultDto<RH_EXP_LABORAL>> Add(RH_EXP_LABORAL entity);
        Task<ResultDto<RH_EXP_LABORAL>> Update(RH_EXP_LABORAL entity);
        Task<string> Delete(int codigoExpLaboral);
        Task<int> GetNextKey();

    }
}

