using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface ISisAuthuserPermissionsRepository
{
    Task<List<AUTH_USER_USER_PERMISSIONS>> GetALL();
    Task<AUTH_USER_USER_PERMISSIONS> GetByID(int id);
    Task<ResultDto<AUTH_USER_USER_PERMISSIONS>> Add(AUTH_USER_USER_PERMISSIONS entity);
    Task<ResultDto<AUTH_USER_USER_PERMISSIONS>> Update(AUTH_USER_USER_PERMISSIONS entity);
    
} 