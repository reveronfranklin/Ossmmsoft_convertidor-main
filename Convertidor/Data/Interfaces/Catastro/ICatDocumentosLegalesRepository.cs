using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatDocumentosLegalesRepository
    {
        Task<List<CAT_DOCUMENTOS_LEGALES>> GetAll();
    }
}
