using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis;

public interface IAuthModelUserServices
{
    Task<List<AuthModelUserAction>> UpdateCachetModelUserAction(int userId);
    Task<ResultDto<UserPermissionDto>> GetUserPermissions(string login);
    Task<List<AuthModelUserAction>> GetModelUserAction(int userId);
    Task<ResultDto<bool>> ValidUserModel(int userId, string model, string action);
    Task<bool> ExistedPermissionInGroup(int userId, int permissionId);
}