using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface IOssAuthUserGroupRepository
{
    Task<List<AUTH_USER_GROUPS>> GetALL();
    Task<AUTH_USER_GROUPS> GetByID(int id);
    Task<List<AUTH_USER_GROUPS>> GetByUser(int userId);
    Task<AUTH_USER_GROUPS> GetByUserGroup(int userId, int groupId);
    Task<ResultDto<AUTH_USER_GROUPS>> Add(AUTH_USER_GROUPS entity);
    Task<ResultDto<AUTH_USER_GROUPS>> Update(AUTH_USER_GROUPS entity);
    Task<string> Delete(int id);
} 