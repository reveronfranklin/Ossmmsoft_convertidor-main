using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface IOssAuthUserGroupRepository
{
    Task<List<AUTH_USER_GROUPS>> GetALL();
    Task<AUTH_USER_GROUPS> GetByID(int id);
    Task<ResultDto<AUTH_USER_GROUPS>> Add(AUTH_USER_GROUPS entity);
    Task<ResultDto<AUTH_USER_GROUPS>> Update(AUTH_USER_GROUPS entity);
    
} 