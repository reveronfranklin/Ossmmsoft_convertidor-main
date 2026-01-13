using Convertidor.Data.EntitiesDestino.ADM;

namespace Convertidor.Data.DestinoInterfaces.ADM;

public interface IAdmComprobantesDocumentosOpDestinoRepository
{
    Task<ResultDto<bool>> Add(List<ADM_COMPROBANTES_DOCUMENTOS_OP> entities);
    Task<string> Delete(int codigoOrdenPago);
  
}