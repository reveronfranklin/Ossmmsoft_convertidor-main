using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmChequesRepository
    {
        Task<ADM_CHEQUES> GetByCodigoCheque(int codigoCheque);
        Task<ResultDto<ADM_CHEQUES>> Add(ADM_CHEQUES entity);
        Task<ResultDto<ADM_CHEQUES>> Update(ADM_CHEQUES entity);
        Task<int> GetNextKey();
        Task<int> GetNextCheque(int numeroChequera, int codigoPresupuesto);
        Task<string> UpdateSearchText(int codigoLote);
        Task<ResultDto<List<ADM_CHEQUES>>> GetByLote(AdmChequeFilterDto filter);
        Task<string> Delete(int codigoCheque);

    }
}
