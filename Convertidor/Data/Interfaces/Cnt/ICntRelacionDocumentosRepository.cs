using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntRelacionDocumentosRepository
    {
        Task<List<CNT_RELACION_DOCUMENTOS>> GetAll();
    }
}
