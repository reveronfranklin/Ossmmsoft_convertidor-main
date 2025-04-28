using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm;

public interface IAdmRetencionesRepository
{
    Task<ADM_RETENCIONES> GetCodigoRetencion(int codigoRetencion);
    Task<List<ADM_RETENCIONES>> GetAll();
    Task<ResultDto<ADM_RETENCIONES>> Add(ADM_RETENCIONES entity);
    Task<ResultDto<ADM_RETENCIONES>> Update(ADM_RETENCIONES entity);
    Task<string> Delete(int codigoRetencion);
    Task<int> GetNextKey();
    Task<ADM_RETENCIONES> GetByExtra1(string extra1);


}