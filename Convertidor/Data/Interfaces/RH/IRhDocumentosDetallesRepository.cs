using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhDocumentosDetallesRepository
    {
        Task<RH_DOCUMENTOS_DETALLES> GetByCodigo(int codigoDocumentoDetalle);
        Task<List<RH_DOCUMENTOS_DETALLES>> GetByCodigoDocumento(int codigoDocumento);
        Task<ResultDto<RH_DOCUMENTOS_DETALLES>> Add(RH_DOCUMENTOS_DETALLES entity);
        Task<ResultDto<RH_DOCUMENTOS_DETALLES>> Update(RH_DOCUMENTOS_DETALLES entity);
        Task<string> Delete(int codigoDocumentoDetalle);
        Task<int> GetNextKey();

    }
}

