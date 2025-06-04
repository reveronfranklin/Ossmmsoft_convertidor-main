using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmBeneficiariosOpRepository
    {
        Task<ADM_BENEFICIARIOS_OP> GetCodigoBeneficiarioOp(int codigoBeneficiarioOp);
        Task<List<ADM_BENEFICIARIOS_OP>> GetAll();
        Task<List<ADM_BENEFICIARIOS_OP>> GetByOrdenPago(int codigoOrdenPago);
        Task<ADM_BENEFICIARIOS_OP> GetByOrdenPagoProveedor(int codigoOrdenPago, int codigoProveedor);
        Task<ResultDto<ADM_BENEFICIARIOS_OP>> Add(ADM_BENEFICIARIOS_OP entity);
        Task<ResultDto<ADM_BENEFICIARIOS_OP>> Update(ADM_BENEFICIARIOS_OP entity);
        Task<string> Delete(int codigoBeneficiarioOp);
        Task<string> UpdateMontoPagado(int codigoBeneficiarioOp, decimal montoPagado);
        Task<string> UpdateMontoAnulado(int codigoOrdenPago);
        Task<int> GetNextKey();
    }
}
