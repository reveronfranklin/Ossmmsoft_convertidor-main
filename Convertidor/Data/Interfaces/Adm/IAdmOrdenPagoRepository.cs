using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmOrdenPagoRepository
    {
        Task<ADM_ORDEN_PAGO> GetCodigoOrdenPago(int codigoOrdenPago);
        Task<List<ADM_ORDEN_PAGO>> GetAll();
        Task<ResultDto<ADM_ORDEN_PAGO>> Add(ADM_ORDEN_PAGO entity);
        Task<ResultDto<ADM_ORDEN_PAGO>> Update(ADM_ORDEN_PAGO entity);
        Task<string> Delete(int codigoOrdenPago);
        Task<int> GetNextKey();
    }
}
