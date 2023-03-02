using Convertidor.Data.Entities;
using Convertidor.Data.EntitiesDestino;
using Convertidor.Dtos;

namespace Convertidor.Services
{
    public interface IHistoricoNominaService
    {
      
        Task<IEnumerable<HistoricoNomina>> GetByLastDay(int days);
        Task<IEnumerable<HistoricoNomina>> GetByLastDayWithRelation(int days);
        Task<List<RH_HISTORICO_NOMINA>> GetByPeriodo(int codigoPeriodo, int tipoNomina);
        Task<ResultDto<HistoricoNomina>> TransferirHistoricoNominaPorCantidadDeDias(int dias);
    }
}
