using Convertidor.Data.EntitiesDestino;

namespace Convertidor.Services
{
    public interface IConceptosRetencionesService
    {
        Task<ResultDto<ConceptosRetenciones>> CrearConceptosRetencionBase();
      
        Task<ConceptosRetenciones> GetByKey(long codigoConcepto, string titulo);
        Task<ConceptosRetenciones> GetByConcepto(long codigoConcepto);
    }
}
