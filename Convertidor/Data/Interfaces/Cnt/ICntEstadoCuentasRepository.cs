using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntEstadoCuentasRepository
    {
        Task<List<CNT_ESTADO_CUENTAS>> GetAll();
    }
}
