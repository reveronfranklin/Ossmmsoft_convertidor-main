using Convertidor.Dtos;
using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis;

public interface IOssFuncionService
{
    Task<ResultDto<OssFuncionResponseDto?>> Update(OssFuncionUpdateDto dto);
    Task<ResultDto<OssFuncionResponseDto>> Create(OssFuncionUpdateDto dto);
    Task<ResultDto<OssFuncionDeleteDto>> Delete(OssFuncionDeleteDto dto);
    Task<ResultDto<OssFuncionResponseDto>> GetByCodigo(OssFuncionFilterDto dto);
    Task<ResultDto<List<OssFuncionResponseDto>>> GetAll();
    
} 