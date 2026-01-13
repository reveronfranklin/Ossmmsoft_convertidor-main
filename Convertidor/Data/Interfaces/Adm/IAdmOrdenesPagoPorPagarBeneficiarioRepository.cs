using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm;

public interface IAdmOrdenesPagoPorPagarBeneficiarioRepository
{
    Task<List<ADM_V_OP_POR_PAGAR_BENE>> GetByOrdenPago(int codigoOrdenPago);

}