using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmPucOrdenPagoRepository
    {
        Task<ADM_PUC_ORDEN_PAGO> GetCodigoPucOrdenPago(int CodigoPucOrdenPago);
        Task<List<ADM_PUC_ORDEN_PAGO>> GetAll();
        Task<ResultDto<ADM_PUC_ORDEN_PAGO>> Add(ADM_PUC_ORDEN_PAGO entity);
        Task<ResultDto<ADM_PUC_ORDEN_PAGO>> Update(ADM_PUC_ORDEN_PAGO entity);
        Task<string> Delete(int CodigoPucOrdenPago);
        Task<int> GetNextKey();
    }
}
