using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface IOssAuthUserRepository
{
    Task<List<AUTH_USER>> GetALL();
    Task<AUTH_USER> GetByID(int id);
    Task<ResultDto<AUTH_USER>> Add(AUTH_USER entity);
    Task<ResultDto<AUTH_USER>> Update(AUTH_USER entity);
    
} 