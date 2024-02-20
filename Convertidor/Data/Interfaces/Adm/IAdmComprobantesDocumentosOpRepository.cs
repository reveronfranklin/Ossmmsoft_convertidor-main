using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmComprobantesDocumentosOpRepository
    {
        Task<ADM_COMPROBANTES_DOCUMENTOS_OP> GetCodigoComprobanteDocOp(int codigoComprobanteDocOp);
        Task<List<ADM_COMPROBANTES_DOCUMENTOS_OP>> GetAll();
        Task<ResultDto<ADM_COMPROBANTES_DOCUMENTOS_OP>> Add(ADM_COMPROBANTES_DOCUMENTOS_OP entity);
        Task<ResultDto<ADM_COMPROBANTES_DOCUMENTOS_OP>> Update(ADM_COMPROBANTES_DOCUMENTOS_OP entity);
        Task<string> Delete(int codigoComprobanteDocOp);
        Task<int> GetNextKey();
    }
}
