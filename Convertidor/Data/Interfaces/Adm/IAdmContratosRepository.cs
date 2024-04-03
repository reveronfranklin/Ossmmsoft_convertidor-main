using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmContratosRepository
    {
        Task<ADM_CONTRATOS> GetByCodigoContrato(int codigoContrato);
        Task<List<ADM_CONTRATOS>> GetAll();
        Task<ResultDto<ADM_CONTRATOS>> Add(ADM_CONTRATOS entity);
        Task<ResultDto<ADM_CONTRATOS>> Update(ADM_CONTRATOS entity);
        Task<string> Delete(int codigoContrato);
        Task<int> GetNextKey();
    }
}
