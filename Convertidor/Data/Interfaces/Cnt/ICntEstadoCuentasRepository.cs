using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntEstadoCuentasRepository
    {
        Task<List<CNT_ESTADO_CUENTAS>> GetAll();
        Task<CNT_ESTADO_CUENTAS> GetByCodigo(int codigoEstadoCuenta);
        Task<ResultDto<CNT_ESTADO_CUENTAS>> Add(CNT_ESTADO_CUENTAS entity);
        Task<ResultDto<CNT_ESTADO_CUENTAS>> Update(CNT_ESTADO_CUENTAS entity);
        Task<int> GetNextKey();
    }
}
