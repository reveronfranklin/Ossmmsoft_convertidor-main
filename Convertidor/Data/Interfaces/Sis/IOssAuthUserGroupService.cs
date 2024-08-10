using Convertidor.Dtos.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface IOssAuthUserGroupService
{
    Task<ResultDto<AuthUserGroupResponseDto>> Create(AuthUserGroupUpdateDto dto);
    Task<ResultDto<AuthUserGroupDeleteDto>> Delete(AuthUserGroupDeleteDto dto);
    Task<ResultDto<AuthUserGroupResponseDto>> GetById(AuthUserGroupFilterDto dto);
    Task<ResultDto<List<AuthUserGroupResponseDto>>> GetByUser(AuthUserGroupFilterDto dto);
    Task<ResultDto<List<AuthUserGroupResponseDto>>> GetAll();
    
}