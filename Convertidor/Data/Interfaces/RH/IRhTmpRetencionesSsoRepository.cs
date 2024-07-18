namespace Convertidor.Data.Interfaces.RH
{
    public interface IRhTmpRetencionesSsoRepository
    {
        Task<List<RH_TMP_RETENCIONES_SSO>> GetByProcesoId(int procesoId);
        Task Add(int procesoId, FilterRetencionesDto filter);
        Task<string> Delete(int procesoId);
    }
}
