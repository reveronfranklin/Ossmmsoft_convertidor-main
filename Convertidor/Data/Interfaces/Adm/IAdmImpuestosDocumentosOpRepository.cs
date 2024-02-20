using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmImpuestosDocumentosOpRepository
    {
        Task<ADM_IMPUESTOS_DOCUMENTOS_OP> GetCodigoImpuestoDocumentoOp(int codigoImpuestoDocumentoOp);
        Task<List<ADM_IMPUESTOS_DOCUMENTOS_OP>> GetAll();
        Task<ResultDto<ADM_IMPUESTOS_DOCUMENTOS_OP>> Add(ADM_IMPUESTOS_DOCUMENTOS_OP entity);
        Task<ResultDto<ADM_IMPUESTOS_DOCUMENTOS_OP>> Update(ADM_IMPUESTOS_DOCUMENTOS_OP entity);
        Task<string> Delete(int codigoImpuestoDocumentoOp);
        Task<int> GetNextKey();
    }
}
