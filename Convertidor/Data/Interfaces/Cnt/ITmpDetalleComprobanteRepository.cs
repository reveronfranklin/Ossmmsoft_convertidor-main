using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ITmpDetalleComprobanteRepository
    {
        Task<List<TMP_DETALLE_COMPROBANTE>> GetAll();
    }
}
