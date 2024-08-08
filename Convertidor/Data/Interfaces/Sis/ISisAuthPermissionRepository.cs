using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface ISisAuthPermissionRepository
{
    Task<List<AUTH_PERMISSION>> GetALL();
    Task<AUTH_PERMISSION> GetByID(int id);
    Task<ResultDto<AUTH_PERMISSION>> Add(AUTH_PERMISSION entity);
    Task<ResultDto<AUTH_PERMISSION>> Update(AUTH_PERMISSION entity);
    
} 