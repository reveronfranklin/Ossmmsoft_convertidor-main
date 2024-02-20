using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis;

public interface IOssVariableService
{
    Task<ResultDto<OssVariableResponseDto?>> Update(OssVariableUpdateDto dto);
    Task<ResultDto<OssVariableResponseDto>> Create(OssVariableUpdateDto dto);
    Task<ResultDto<OssVariableDeleteDto>> Delete(OssVariableDeleteDto dto);
    Task<ResultDto<OssVariableResponseDto>> GetById(OssVariableFilterDto dto);
    Task<ResultDto<OssVariableResponseDto>> GetByCodigo(OssVariableFilterDto dto);
    Task<ResultDto<List<OssVariableResponseDto>>> GetAll();
    
}