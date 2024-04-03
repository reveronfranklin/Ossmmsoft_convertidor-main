using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmPucContratoRepository
    {
        Task<ADM_PUC_CONTRATO> GetCodigoPucContrato(int codigoPucContrato);
        Task<List<ADM_PUC_CONTRATO>> GetAll();
        Task<ResultDto<ADM_PUC_CONTRATO>> Add(ADM_PUC_CONTRATO entity);
        Task<ResultDto<ADM_PUC_CONTRATO>> Update(ADM_PUC_CONTRATO entity);
        Task<string> Delete(int codigoPucContrato);
        Task<int> GetNextKey();

    }
}
