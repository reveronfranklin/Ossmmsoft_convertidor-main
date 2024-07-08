using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntDetalleComprobanteRepository
    {
        Task<List<CNT_DETALLE_COMPROBANTE>> GetAll();
    }
}
