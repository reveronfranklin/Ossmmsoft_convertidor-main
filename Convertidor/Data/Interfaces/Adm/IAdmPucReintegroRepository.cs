using Convertidor.Data.Entities.ADM;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmPucReintegroRepository
    {
        Task<ADM_PUC_REINTEGRO> GetCodigoPucReintegro(int codigoPucReintegro);
        Task<List<ADM_PUC_REINTEGRO>> GetAll();
        Task<ResultDto<ADM_PUC_REINTEGRO>> Add(ADM_PUC_REINTEGRO entity);
        Task<ResultDto<ADM_PUC_REINTEGRO>> Update(ADM_PUC_REINTEGRO entity);
        Task<string> Delete(int codigoPucReintegro);
        Task<int> GetNextKey();
    }
}
