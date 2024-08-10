using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface IOssAuthContentTypeRepository
{
    Task<List<AUTH_CONTENT_TYPE>> GetALL();
    Task<AUTH_CONTENT_TYPE> GetByID(int id);
    Task<ResultDto<AUTH_CONTENT_TYPE>> Add(AUTH_CONTENT_TYPE entity);
    Task<ResultDto<AUTH_CONTENT_TYPE>> Update(AUTH_CONTENT_TYPE entity);
    Task<string> Delete(int id);
}