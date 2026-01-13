using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntDetalleEdoCtaRepository
    {

        Task<List<CNT_DETALLE_EDO_CTA>> GetAll();
        Task<List<CNT_DETALLE_EDO_CTA>> GetByCodigoEstadoCuenta(int codigoEstadoCuenta);
        Task<CNT_DETALLE_EDO_CTA> GetByCodigo(int codigoDetalleEdoCuenta);
        Task<ResultDto<CNT_DETALLE_EDO_CTA>> Add(CNT_DETALLE_EDO_CTA entity);
        Task<ResultDto<CNT_DETALLE_EDO_CTA>> Update(CNT_DETALLE_EDO_CTA entity);
        Task<string> Delete(int codigoDetalleEdoCuenta);
        Task<int> GetNextKey();
    }
}
