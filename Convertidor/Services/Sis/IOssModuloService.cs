using Convertidor.Dtos;
using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis;

public interface IOssModuloService
{
    Task<ResultDto<OssModuloResponseDto>> GetByCodigo(OssModuloFilterDto dto);
    Task<ResultDto<List<OssModuloResponseDto>>> GetAll();
    Task<ResultDto<OssModuloResponseDto?>> Update(OssModuloUpdateDto dto);
    Task<ResultDto<OssModuloResponseDto>> Create(OssModuloUpdateDto dto);
    Task<ResultDto<OssModuloDeletedto>> Delete(OssModuloDeletedto dto);
  
    
}