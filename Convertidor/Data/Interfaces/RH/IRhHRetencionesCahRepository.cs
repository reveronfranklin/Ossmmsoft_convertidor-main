namespace Convertidor.Data.Interfaces.RH
{
    public interface IRhHRetencionesCahRepository
    {
        Task<List<RH_H_RETENCIONES_CAH>> GetByProcesoId(int procesoId);
        Task<ResultDto<RH_H_RETENCIONES_CAH>> Add(RH_H_RETENCIONES_CAH entity);
        Task<string> Delete(int procesoId);

        Task<List<RH_H_RETENCIONES_CAH>> GetByTipoNominaFechaDesdeFachaHasta(int tipoNomina, string fechaDesde, string fechaHasta);

        Task<int> GetNextKey();
    }
}
