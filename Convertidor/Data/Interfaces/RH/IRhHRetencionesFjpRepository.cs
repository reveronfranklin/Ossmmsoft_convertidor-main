namespace Convertidor.Data.Interfaces.RH
{
    public interface IRhHRetencionesFjpRepository
    {
        Task<List<RH_H_RETENCIONES_FJP>> GetByProcesoId(int procesoId);
        Task<ResultDto<RH_H_RETENCIONES_FJP>> Add(RH_H_RETENCIONES_FJP entity);
        Task<string> Delete(int procesoId);

        Task<List<RH_H_RETENCIONES_FJP>> GetByTipoNominaFechaDesdeFachaHasta(int tipoNomina, string fechaDesde, string fechaHasta);

        Task<int> GetNextKey();
    }
}
