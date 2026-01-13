using Convertidor.Data.EntitiesDestino.ADM;

namespace Convertidor.Data.DestinoInterfaces.ADM;

public interface IAdmRetencionesOpDestinoRepository
{
    Task<ResultDto<bool>> Add(List<ADM_RETENCIONES_OP> entities);
    Task<string> Delete(int codigoOrdenPago);
}