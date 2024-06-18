namespace Convertidor.Data.Interfaces.RH
{
    public interface IRhHRetencionesSindRepository
    {
        Task<List<RH_H_RETENCIONES_SIND>> GetByProcesoId(int procesoId);
        Task<ResultDto<RH_H_RETENCIONES_SIND>> Add(RH_H_RETENCIONES_SIND entity);
        Task<string> Delete(int procesoId);

        Task<List<RH_H_RETENCIONES_SIND>> GetByTipoNominaFechaDesdeFachaHasta(int tipoNomina, string fechaDesde, string fechaHasta);

        Task<int> GetNextKey();
    }
}
