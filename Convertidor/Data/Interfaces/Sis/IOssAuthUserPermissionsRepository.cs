using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface IOssAuthUserPermissionsRepository
{
    Task<List<AUTH_USER_USER_PERMISSIONS>> GetALL();
    Task<AUTH_USER_USER_PERMISSIONS> GetByID(int id);
    Task<AUTH_USER_USER_PERMISSIONS> GetByUserPermision(int userId, int permissionId);
    Task<ResultDto<AUTH_USER_USER_PERMISSIONS>> Add(AUTH_USER_USER_PERMISSIONS entity);
    Task<ResultDto<AUTH_USER_USER_PERMISSIONS>> Update(AUTH_USER_USER_PERMISSIONS entity);
    Task<string> Delete(int id);
    Task<List<AUTH_USER_USER_PERMISSIONS>> GetByUser(int userId);
    Task<ResultDto<List<AuthUserPermisionResponseDto>>> GetByUser(AuthUserPermisionFilterDto dto);
} 