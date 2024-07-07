using Convertidor.Data.Entities;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntComprobantesRepository
    {
        Task<List<CNT_COMPROBANTES>> GetAll();
    }
}
