using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ITmpDetalleComprobanteRepository
    {
        Task<List<TMP_DETALLE_COMPROBANTE>> GetAll();
        Task<TMP_DETALLE_COMPROBANTE> GetByCodigo(int codigoDetalleComprobante);
        Task<ResultDto<TMP_DETALLE_COMPROBANTE>> Add(TMP_DETALLE_COMPROBANTE entity);
        Task<ResultDto<TMP_DETALLE_COMPROBANTE>> Update(TMP_DETALLE_COMPROBANTE entity);
        Task<int> GetNextKey();
    }
}
