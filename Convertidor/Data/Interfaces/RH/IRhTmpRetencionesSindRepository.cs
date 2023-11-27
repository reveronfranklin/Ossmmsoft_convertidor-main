using Convertidor.Data.Entities.Rh;

namespace Convertidor.Data.Interfaces.RH
{
    public interface IRhTmpRetencionesSindRepository
    {
        Task<List<RH_TMP_RETENCIONES_SIND>> GetByProcesoId(int procesoId);
        Task Add(int procesoId, int tipoNomina, string fechaDesde, string fechaHasta);
        Task<string> Delete(int procesoId);
    }
}
