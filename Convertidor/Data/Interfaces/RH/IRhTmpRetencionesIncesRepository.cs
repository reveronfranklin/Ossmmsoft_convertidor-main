namespace Convertidor.Data.Interfaces.RH
{
    public interface IRhTmpRetencionesIncesRepository
    {
        Task<List<RH_TMP_RETENCIONES_INCES>> GetByProcesoId(int procesoId);
        Task Add(int procesoId, FilterRetencionesDto filter);
        Task<string> Delete(int procesoId);
    }
}
