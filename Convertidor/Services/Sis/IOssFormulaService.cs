using Convertidor.Dtos;
using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis;

public interface IOssFormulaService
{
    Task<ResultDto<OssFormulaResponseDto?>> Update(OssFormulaUpdateDto dto);
    Task<ResultDto<OssFormulaResponseDto>> Create(OssFormulaUpdateDto dto);
    Task<ResultDto<OssFormulaDeleteDto>> Delete(OssFormulaDeleteDto dto);
    Task<ResultDto<OssFormulaResponseDto>> GetById(OssFormulaFilterDto dto);
    Task<ResultDto<List<OssFormulaResponseDto>>> GetAll();
    Task<ResultDto<List<OssFormulaResponseDto>>> GetByIdModeloCalculo(OssFormulaFilterDto dto);

}