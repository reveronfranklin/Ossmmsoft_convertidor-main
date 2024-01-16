using Convertidor.Dtos;
using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis;

public interface IOssCalculoService
{
    Task<ResultDto<OssCalculoResponseDto?>> Update(OssCalculoUpdateDto dto);
    Task<ResultDto<OssCalculoResponseDto>> Create(OssCalculoUpdateDto dto, int idCalculo);
    Task<ResultDto<OssCalculoDeleteDto>> Delete(OssCalculoDeleteDto dto);
    Task<ResultDto<OssCalculoResponseDto>> GetById(OssFormulaFilterDto dto);
    Task<ResultDto<List<OssCalculoResponseDto>>> GetByIdCalculo(OssFormulaFilterDto dto);
    Task<int> GetNextCalculo();
    
}