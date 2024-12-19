using Convertidor.Data.EntitiesDestino.ADM;

namespace Convertidor.Data.DestinoInterfaces.ADM;

public interface IAdmRetencionesDestinoRepository
{
    Task<ADM_RETENCIONES> GetCodigoRetencion(int codigoRetencion);
    Task<List<ADM_RETENCIONES>> GetAll();
    Task<ResultDto<ADM_RETENCIONES>> Add(ADM_RETENCIONES entity);
    Task<ResultDto<ADM_RETENCIONES>> Update(ADM_RETENCIONES entity);
    Task<string> Delete(int codigoRetencion);
    Task<int> GetNextKey();

}