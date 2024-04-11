using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto;

public interface IPreResumenSaldoRepository
{
    Task<List<PRE_RESUMEN_SALDOS>> GetAllByPresupuesto(int codigoPresupuesto);
    
}