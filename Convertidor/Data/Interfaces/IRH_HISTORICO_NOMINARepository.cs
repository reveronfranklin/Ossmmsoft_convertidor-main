using Convertidor.Data.Entities;

namespace Convertidor.Data.Interfaces
{
    public interface IRH_HISTORICO_NOMINARepository
    {

        Task<IEnumerable<RH_HISTORICO_NOMINA>> Get();
        Task<IEnumerable<RH_HISTORICO_NOMINA>> GetByLastDay(int days);
        Task<List<RH_HISTORICO_NOMINA>> GetByPeriodo(int codigoPeriodo, int tipoNomina);
    }
}
