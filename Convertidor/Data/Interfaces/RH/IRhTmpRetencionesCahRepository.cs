using Convertidor.Data.Entities.Rh;

namespace Convertidor.Data.Interfaces.RH
{
    public interface IRhTmpRetencionesCahRepository
    {
        Task<List<RH_TMP_RETENCIONES_CAH>> GetByProcesoId(int procesoId);
        Task Add(int procesoId, int tipoNomina, string fechaDesde, string fechaHasta);
        Task<string> Delete(int procesoId);
    }
}
