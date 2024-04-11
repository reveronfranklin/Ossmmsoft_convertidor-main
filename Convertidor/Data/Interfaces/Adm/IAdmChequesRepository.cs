using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmChequesRepository
    {
        Task<ADM_CHEQUES> GetByCodigoCheque(int codigoCheque);
        Task<List<ADM_CHEQUES>> GetAll();
        Task<ResultDto<ADM_CHEQUES>> Add(ADM_CHEQUES entity);
        Task<ResultDto<ADM_CHEQUES>> Update(ADM_CHEQUES entity);
        Task<string> Delete(int codigoCheque);
        Task<int> GetNextKey();

    }
}
