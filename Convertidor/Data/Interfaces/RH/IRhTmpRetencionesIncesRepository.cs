namespace Convertidor.Data.Interfaces.RH
{
    public interface IRhTmpRetencionesIncesRepository
    {
        Task<List<RH_TMP_RETENCIONES_INCES>> GetByProcesoId(int procesoId);
        Task Add(int procesoId, int tipoNomina, string fechaDesde, string fechaHasta);
        Task<string> Delete(int procesoId);
    }
}
