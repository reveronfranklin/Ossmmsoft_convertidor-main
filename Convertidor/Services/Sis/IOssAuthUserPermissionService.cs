using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis;

public interface IOssAuthUserPermissionService
{
    Task<ResultDto<AuthUserPermisionResponseDto>> Create(AuthUserPermisionUpdateDto dto);
    Task<ResultDto<AuthUserPermisionDeleteDto>> Delete(AuthUserPermisionDeleteDto dto);
    Task<ResultDto<AuthUserPermisionResponseDto>> GetById(AuthUserPermisionFilterDto dto);
    Task<ResultDto<List<AuthUserPermisionResponseDto>>> GetByUser(AuthUserPermisionFilterDto dto);
    Task<ResultDto<List<AuthUserPermisionResponseDto>>> GetAll();
    
}