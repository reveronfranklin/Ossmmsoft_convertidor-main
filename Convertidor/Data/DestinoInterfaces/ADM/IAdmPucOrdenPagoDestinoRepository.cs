using Convertidor.Data.EntitiesDestino.ADM;

namespace Convertidor.Data.DestinoInterfaces.ADM;

public interface IAdmPucOrdenPagoDestinoRepository
{
    Task<ResultDto<bool>> Add(List<ADM_PUC_ORDEN_PAGO> entities);
    Task<string> Delete(int codigoOrdenPago);
}