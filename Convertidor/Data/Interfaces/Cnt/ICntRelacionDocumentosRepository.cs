using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntRelacionDocumentosRepository
    {
        Task<List<CNT_RELACION_DOCUMENTOS>> GetAll();
        Task<CNT_RELACION_DOCUMENTOS> GetByCodigo(int codigoRelacionDocumento);
        Task<ResultDto<CNT_RELACION_DOCUMENTOS>> Add(CNT_RELACION_DOCUMENTOS entity);
        Task<ResultDto<CNT_RELACION_DOCUMENTOS>> Update(CNT_RELACION_DOCUMENTOS entity);
        Task<string> Delete(int codigoRelaciondocumento);
        Task<int> GetNextKey();
    }
}
