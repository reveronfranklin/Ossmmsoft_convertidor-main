using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatDocumentosLegalesRepository
    {
        Task<List<CAT_DOCUMENTOS_LEGALES>> GetAll();
        Task<ResultDto<CAT_DOCUMENTOS_LEGALES>> Add(CAT_DOCUMENTOS_LEGALES entity);
        Task<int> GetNextKey();

    }
}
