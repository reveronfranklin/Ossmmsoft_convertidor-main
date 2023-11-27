using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.RH
{
    public interface IRhHRetencionesIncesRepository
    {
        Task<List<RH_H_RETENCIONES_INCES>> GetByProcesoId(int procesoId);
        Task<ResultDto<RH_H_RETENCIONES_INCES>> Add(RH_H_RETENCIONES_INCES entity);
        Task<string> Delete(int procesoId);

        Task<List<RH_H_RETENCIONES_INCES>> GetByTipoNominaFechaDesdeFachaHasta(int tipoNomina, string fechaDesde, string fechaHasta);

        Task<int> GetNextKey();
    }
}
