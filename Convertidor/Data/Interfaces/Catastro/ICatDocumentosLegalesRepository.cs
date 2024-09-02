using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatDocumentosLegalesRepository
    {
        Task<List<CAT_DOCUMENTOS_LEGALES>> GetAll();
        Task<CAT_DOCUMENTOS_LEGALES> GetByCodigo(int codigoDocumentosLegales);
        Task<ResultDto<CAT_DOCUMENTOS_LEGALES>> Add(CAT_DOCUMENTOS_LEGALES entity);
        Task<ResultDto<CAT_DOCUMENTOS_LEGALES>> Update(CAT_DOCUMENTOS_LEGALES entity);
        Task<string> Delete(int codigoDocumentosLegales);
        Task<int> GetNextKey();

    }
}
