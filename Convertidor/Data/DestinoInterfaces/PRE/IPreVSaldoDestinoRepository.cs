using Convertidor.Data.EntitiesDestino.PRE;

namespace Convertidor.Data.DestinoInterfaces.PRE;

public interface IPreVSaldoDestinoRepository
{
    Task<ResultDto<bool>> Add(PRE_V_SALDOS entities);
    Task<string> Delete(int codigoPresupuesto, int codigoSaldo);
}