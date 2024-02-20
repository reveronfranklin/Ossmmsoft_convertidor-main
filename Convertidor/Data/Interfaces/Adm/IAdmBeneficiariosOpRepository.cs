using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmBeneficiariosOpRepository
    {
        Task<ADM_BENEFICIARIOS_OP> GetCodigoBeneficiarioOp(int codigoBeneficiarioOp);
        Task<List<ADM_BENEFICIARIOS_OP>> GetAll();
        Task<ResultDto<ADM_BENEFICIARIOS_OP>> Add(ADM_BENEFICIARIOS_OP entity);
        Task<ResultDto<ADM_BENEFICIARIOS_OP>> Update(ADM_BENEFICIARIOS_OP entity);
        Task<string> Delete(int codigoBeneficiarioOp);
        Task<int> GetNextKey();
    }
}
