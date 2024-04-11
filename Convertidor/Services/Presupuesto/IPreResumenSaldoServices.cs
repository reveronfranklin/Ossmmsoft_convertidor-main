using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto;

public interface IPreResumenSaldoServices
{
   Task<ResultDto<List<PreResumenSaldoGetDto>>> GetAllByPresupuesto(int codigoPresupuesto);

}