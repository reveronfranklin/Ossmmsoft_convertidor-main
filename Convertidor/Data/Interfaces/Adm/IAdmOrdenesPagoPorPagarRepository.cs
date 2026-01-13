using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm;

public interface IAdmOrdenesPagoPorPagarRepository
{
    Task<List<ADM_V_OP_POR_PAGAR>> GetAll(int codigoPresupuesto);
}