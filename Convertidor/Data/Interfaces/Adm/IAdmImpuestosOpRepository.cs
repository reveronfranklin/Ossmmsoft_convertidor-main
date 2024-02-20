using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmImpuestosOpRepository
    {
        Task<ADM_IMPUESTOS_OP> GetCodigoImpuestoOp(int codigoImpuestoOp);
        Task<ADM_IMPUESTOS_OP> GetByOrdenDePagoTipoImpuesto(int codigoOrdenDePago, int tipoImpuestoId);
        Task<List<ADM_IMPUESTOS_OP>> GetAll();
        Task<ResultDto<ADM_IMPUESTOS_OP>> Add(ADM_IMPUESTOS_OP entity);
        Task<ResultDto<ADM_IMPUESTOS_OP>> Update(ADM_IMPUESTOS_OP entity);
        Task<string> Delete(int codigoImpuestoOp);
        Task<int> GetNextKey();
    }
}
