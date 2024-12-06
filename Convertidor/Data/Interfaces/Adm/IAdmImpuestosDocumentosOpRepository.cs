using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmImpuestosDocumentosOpRepository
    {
        Task<ADM_IMPUESTOS_DOCUMENTOS_OP> GetCodigoImpuestoDocumentoOp(int codigoImpuestoDocumentoOp);
        Task<List<ADM_IMPUESTOS_DOCUMENTOS_OP>> GetAll();
        Task<List<ADM_IMPUESTOS_DOCUMENTOS_OP>> GetByDocumento(int codigoDocumento);
        Task<ResultDto<ADM_IMPUESTOS_DOCUMENTOS_OP>> Add(ADM_IMPUESTOS_DOCUMENTOS_OP entity);
        Task<ResultDto<ADM_IMPUESTOS_DOCUMENTOS_OP>> Update(ADM_IMPUESTOS_DOCUMENTOS_OP entity);
        Task<string> Delete(int codigoImpuestoDocumentoOp);
        Task<int> GetNextKey();
    }
}
