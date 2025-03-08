namespace Convertidor.Data.Interfaces.RH;

public interface IRhDocumentosAdjuntosRepository
{
    Task<RH_DOCUMENTOS_ADJUNTOS> GetByCodigo(int codigoDocumentoAdjunto);
    Task<List<RH_DOCUMENTOS_ADJUNTOS>> GetByCodigoDocumento(int codigoDocumento);
    Task<RH_DOCUMENTOS_ADJUNTOS> GetByNumeroDocumentoAdjunto(int numeroDocumento, string adjunto);
    Task<ResultDto<RH_DOCUMENTOS_ADJUNTOS>> Add(RH_DOCUMENTOS_ADJUNTOS entity);
    Task<ResultDto<RH_DOCUMENTOS_ADJUNTOS>> Update(RH_DOCUMENTOS_ADJUNTOS entity);
    Task<string> Delete(int codigoDocumentoAdjunto);
    Task<int> GetNextKey();
    
}