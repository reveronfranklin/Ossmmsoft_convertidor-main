using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmHOrdenPagoRepository
    {
        Task<ADM_H_ORDEN_PAGO> GetCodigoHOrdenPago(int codigoHOrdenPago);
        Task<List<ADM_H_ORDEN_PAGO>> GetAll();
        Task<ResultDto<ADM_H_ORDEN_PAGO>> Add(ADM_H_ORDEN_PAGO entity);
        Task<ResultDto<ADM_H_ORDEN_PAGO>> Update(ADM_H_ORDEN_PAGO entity);
        Task<string> Delete(int codigoHOrdenPago);
        Task<int> GetNextKey();
      
    }
}
