using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm;

public interface IAdmPagosNotasTercerosRepository
{
    Task<List<ADM_V_NOTAS_TERCEROS>> GetByLote(int codigoLote);
    Task<List<ADM_V_NOTAS_TERCEROS>> GetByLoteCodigoPago(int codigoLote, int codigoPago);
}