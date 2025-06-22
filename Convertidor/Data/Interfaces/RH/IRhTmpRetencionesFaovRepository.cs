namespace Convertidor.Data.Interfaces.RH
{
    public interface IRhTmpRetencionesFaovRepository
    {
        Task<List<RH_TMP_RETENCIONES_FAOV>> GetByProcesoId(int procesoId);


        Task<ResultDto<List<RH_TMP_RETENCIONES_FAOV>>> Add(int procesoId, int tipoNomina, string fechaDesde,
            string fechaHasta);
        Task<string> Delete(int procesoId);
    }
}
