namespace Convertidor.Data.Interfaces.RH
{
    public interface IRhHRetencionesFaovRepository
    {
        Task<List<RH_H_RETENCIONES_FAOV>> GetByProcesoId(int procesoId);
        Task<ResultDto<RH_H_RETENCIONES_FAOV>> Add(RH_H_RETENCIONES_FAOV entity);
        Task<string> Delete(int procesoId);

        Task<List<RH_H_RETENCIONES_FAOV>> GetByTipoNominaFechaDesdeFachaHasta(int tipoNomina, string fechaDesde, string fechaHasta);

        Task<int> GetNextKey();
    }
}
