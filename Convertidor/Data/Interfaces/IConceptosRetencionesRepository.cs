using Convertidor.Data.EntitiesDestino;

namespace Convertidor.Data.Interfaces
{
    public interface IConceptosRetencionesRepository
    {
        Task<bool> Add(ConceptosRetenciones entity);
        Task<bool> Add(List<ConceptosRetenciones> entities);
        Task<List<ConceptosRetenciones>> GetAll();
        Task<ConceptosRetenciones> GetByKey(long codigoConcepto, string titulo);
        Task DeleteAll();
        Task<ConceptosRetenciones> GetByConcepto(long codigoConcepto);
    }
}
