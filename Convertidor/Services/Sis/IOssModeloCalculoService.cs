using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis;

public interface IOssModeloCalculoService
{
    Task<ResultDto<OssModeloCalculoResponseDto?>> Update(OssModeloCalculoUpdateDto dto);
    Task<ResultDto<OssModeloCalculoResponseDto>> Create(OssModeloCalculoUpdateDto dto);
    Task<ResultDto<OssModeloCalculoDeleteDto>> Delete(OssModeloCalculoDeleteDto dto);
    Task<ResultDto<OssModeloCalculoResponseDto>> GetByCodigo(OssModeloCalculoFilterDto dto);
    Task<ResultDto<List<OssModeloCalculoResponseDto>>> GetAll();
    

}