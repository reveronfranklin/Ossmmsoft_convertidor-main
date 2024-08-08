using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface ISisAuthGroupRepository
{
    Task<List<AUTH_GROUP>> GetALL();
    Task<AUTH_GROUP> GetByID(int id);
    Task<ResultDto<AUTH_GROUP>> Add(AUTH_GROUP entity);
    Task<ResultDto<AUTH_GROUP>> Update(AUTH_GROUP entity);
    
} 