using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis;

public interface IOssAuthPermissionsService
{
    Task<ResultDto<AuthPermissionResponseDto>> Update(AuthPermissionUpdateDto dto);
    Task<ResultDto<AuthPermissionResponseDto>> Create(AuthPermissionUpdateDto dto);
    Task<ResultDto<AuthPermissionDeleteDto>> Delete(AuthPermissionDeleteDto dto);
    Task<ResultDto<AuthPermissionResponseDto>> GetById(AuthPermissionFilterDto dto);
    Task<ResultDto<List<AuthPermissionResponseDto>>> GetAll();
    List<string> GetListCodeName();

}