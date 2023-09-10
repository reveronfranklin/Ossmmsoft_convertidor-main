using Convertidor.Data.Entities;

namespace Convertidor.Data.Interfaces
{
    public interface IRH_HISTORICO_PERSONAL_CARGORepository
    {

        Task<IEnumerable<RH_HISTORICO_PERSONAL_CARGO>> GetByLastDay(int days);
        Task<RH_HISTORICO_PERSONAL_CARGO> GetPrimerMovimientoByCodigoPersona(int codigoPersona);
        Task<IEnumerable<RH_HISTORICO_PERSONAL_CARGO>> GetByCodigoPersona(int codigoPersona);
    }
}
