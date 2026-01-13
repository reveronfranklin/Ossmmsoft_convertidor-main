using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis;

public interface IOssAuthContentTypeService
{
    Task<ResultDto<AuthContentTypeResponseDto>> Update(AuthContentTypeUpdateDto dto);
    Task<ResultDto<AuthContentTypeResponseDto>> Create(AuthContentTypeUpdateDto dto);
    Task<ResultDto<AuthContentTypeDeleteDto>> Delete(AuthContentTypeDeleteDto dto);
    Task<ResultDto<AuthContentTypeResponseDto>> GetById(AuthContentTypeFilterDto dto);
    Task<ResultDto<List<AuthContentTypeResponseDto>>> GetAll();
    
}