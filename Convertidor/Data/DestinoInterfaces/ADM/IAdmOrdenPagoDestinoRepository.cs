

using Convertidor.Data.EntitiesDestino.ADM;

namespace Convertidor.Data.DestinoInterfaces.ADM
{
    public interface IAdmOrdenPagoDestinoRepository
    {
        Task<ADM_ORDEN_PAGO> GetCodigoOrdenPago(int codigoOrdenPago); 
        Task<ResultDto<ADM_ORDEN_PAGO>> Add(ADM_ORDEN_PAGO entity); 
        Task<string> Delete(int codigoOrdenPago);
     
    }
}
