using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis;

public interface IOssAuthGroupService
{
    Task<ResultDto<AuthGroupResponseDto>> Update(AuthGroupUpdateDto dto);
    Task<ResultDto<AuthGroupResponseDto>> Create(AuthGroupUpdateDto dto);
    Task<ResultDto<AuthGroupDeleteDto>> Delete(AuthGroupDeleteDto dto);
    Task<ResultDto<AuthGroupResponseDto>> GetById(AuthGroupFilterDto dto);
    Task<ResultDto<List<AuthGroupResponseDto>>> GetAll();
 

}