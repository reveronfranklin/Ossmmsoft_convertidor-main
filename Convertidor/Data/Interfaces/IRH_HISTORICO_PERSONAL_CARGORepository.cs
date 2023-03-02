using Convertidor.Data.Entities;

namespace Convertidor.Data.Interfaces
{
    public interface IRH_HISTORICO_PERSONAL_CARGORepository
    {

        Task<IEnumerable<RH_HISTORICO_PERSONAL_CARGO>> GetByLastDay(int days);
    }
}
