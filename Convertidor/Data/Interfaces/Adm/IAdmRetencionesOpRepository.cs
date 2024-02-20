using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmRetencionesOpRepository
    {
        Task<ADM_RETENCIONES_OP> GetCodigoRetencionOp(int codigoRetencionOp);
        Task<List<ADM_RETENCIONES_OP>> GetAll();
        Task<ResultDto<ADM_RETENCIONES_OP>> Add(ADM_RETENCIONES_OP entity);
        Task<ResultDto<ADM_RETENCIONES_OP>> Update(ADM_RETENCIONES_OP entity);
        Task<string> Delete(int codigoRetencionOp);
        Task<int> GetNextKey();
    }
}
