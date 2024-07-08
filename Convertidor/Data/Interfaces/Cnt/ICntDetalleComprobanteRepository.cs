using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntDetalleComprobanteRepository
    {
        Task<List<CNT_DETALLE_COMPROBANTE>> GetAll();
        Task<List<CNT_DETALLE_COMPROBANTE>> GetByCodigoComprobante(int codigoComprobante);
        Task<ResultDto<CNT_DETALLE_COMPROBANTE>> Add(CNT_DETALLE_COMPROBANTE entity);
        Task<int> GetNextKey();
    }
}
