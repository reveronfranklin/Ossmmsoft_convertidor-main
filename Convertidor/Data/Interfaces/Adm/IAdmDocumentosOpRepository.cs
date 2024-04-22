using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmDocumentosOpRepository
    {
        Task<ADM_DOCUMENTOS_OP> GetCodigoDocumentoOp(int codigoDocumentoOp);
        Task<List<ADM_DOCUMENTOS_OP>> GetAll();
        Task<ResultDto<ADM_DOCUMENTOS_OP>> Add(ADM_DOCUMENTOS_OP entity);
        Task<ResultDto<ADM_DOCUMENTOS_OP>> Update(ADM_DOCUMENTOS_OP entity);
        Task<string> Delete(int codigoDocumentoOp);
        Task<int> GetNextKey();
    }
}
