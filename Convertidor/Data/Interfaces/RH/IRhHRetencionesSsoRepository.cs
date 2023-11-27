using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.RH
{
    public interface IRhHRetencionesSsoRepository
    {
        Task<List<RH_H_RETENCIONES_SSO>> GetByProcesoId(int procesoId);
        Task<ResultDto<RH_H_RETENCIONES_SSO>> Add(RH_H_RETENCIONES_SSO entity);
        Task<string> Delete(int procesoId);

        Task<List<RH_H_RETENCIONES_SSO>> GetByTipoNominaFechaDesdeFachaHasta(int tipoNomina, string fechaDesde, string fechaHasta);

        Task<int> GetNextKey();
    }
}
