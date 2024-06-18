using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPreParticipaFinacieraOrgRepository
    {
        Task<PRE_PARTICIPA_FINANCIERA_ORG> GetByCodigo(int codigoParticipaFinancOrg);
        Task<List<PRE_PARTICIPA_FINANCIERA_ORG>> GetAll();
        Task<ResultDto<PRE_PARTICIPA_FINANCIERA_ORG>> Add(PRE_PARTICIPA_FINANCIERA_ORG entity);
        Task<ResultDto<PRE_PARTICIPA_FINANCIERA_ORG>> Update(PRE_PARTICIPA_FINANCIERA_ORG entity);
        Task<string> Delete(int codigoParticipaFinancOrg);
        Task<int> GetNextKey();
    }
}

