using Convertidor.Data.EntitiesDestino.ADM;

namespace Convertidor.Data.DestinoInterfaces.ADM;

public interface IAdmDocumentosOpDestinoRepository
{
    Task<ADM_DOCUMENTOS_OP> GetCodigoDocumentoOp(int codigoDocumentoOp);
    Task<List<ADM_DOCUMENTOS_OP>> GetByCodigoOrdenPago(int codigoOrdenPago);
    Task<List<ADM_DOCUMENTOS_OP>> GetAll();
    Task<ResultDto<ADM_DOCUMENTOS_OP>> Add(ADM_DOCUMENTOS_OP entity);
    Task<ResultDto<ADM_DOCUMENTOS_OP>> Update(ADM_DOCUMENTOS_OP entity);
    Task<string> Delete(int codigoDocumentoOp);
    Task<string> DeleteByOrdenPago(int codigoOrdenPago);
    Task<int> GetNextKey();
}