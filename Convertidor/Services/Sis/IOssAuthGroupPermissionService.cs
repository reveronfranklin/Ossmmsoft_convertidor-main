using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis;

public interface IOssAuthGroupPermissionService
{
    Task<ResultDto<AuthGroupPermisionResponseDto>> Create(AuthGroupPermissionUpdateDto dto);
    Task<ResultDto<AuthGroupPermissionDeleteDto>> Delete(AuthGroupPermissionDeleteDto dto);
    Task<ResultDto<AuthGroupPermisionResponseDto>> GetById(AuthGroupPermissionFilterDto dto);

    Task<ResultDto<List<AuthGroupPermisionResponseDto>>> GetAll();
    Task<ResultDto<List<AuthGroupPermisionResponseDto>>> GetByGroup(AuthGroupPermissionFilterDto dto);


}