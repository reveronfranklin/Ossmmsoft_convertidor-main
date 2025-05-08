using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm;

public interface IAdmBeneficiariosPagosRepository
{
    Task<ADM_BENEFICIARIOS_CH> GetCodigoBeneficiarioPago(int codigoBeneficiarioPago);
    Task<ADM_BENEFICIARIOS_CH> GetByPago(int codigoPago);
    Task<List<ADM_BENEFICIARIOS_CH>> GetAll();

    Task<ResultDto<ADM_BENEFICIARIOS_CH>> Add(ADM_BENEFICIARIOS_CH entity);
    Task<ResultDto<ADM_BENEFICIARIOS_CH>> Update(ADM_BENEFICIARIOS_CH entity);
    Task<string> Delete(int codigoBeneficiarioPago);
    Task<string> DeleteByCodigoPago(int codigoPago);
    Task<int> GetNextKey();
}