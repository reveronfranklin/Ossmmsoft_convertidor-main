using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntReversoConciliacionRepository
    {
        Task<List<CNT_REVERSO_CONCILIACION>> GetAll();
        Task<List<CNT_REVERSO_CONCILIACION>> GetByCodigoConciliacion(int codigoConciliacion);
    }
}
