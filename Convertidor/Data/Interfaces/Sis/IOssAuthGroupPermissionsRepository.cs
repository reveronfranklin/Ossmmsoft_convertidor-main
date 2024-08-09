using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface IOssAuthGroupPermissionsRepository
{
    Task<List<AUTH_GROUP_PERMISSIONS>> GetALL();
    Task<AUTH_GROUP_PERMISSIONS> GetByID(int id);
    Task<ResultDto<AUTH_GROUP_PERMISSIONS>> Add(AUTH_GROUP_PERMISSIONS entity);
    Task<ResultDto<AUTH_GROUP_PERMISSIONS>> Update(AUTH_GROUP_PERMISSIONS entity);
    
} 