using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh;

public interface IRhConceptosFormulaService
{
    Task<ResultDto<RhConceptosFormulaResponseDto?>> Update(RhConceptosFormulaUpdateDto dto);
    Task<ResultDto<RhConceptosFormulaResponseDto>> Create(RhConceptosFormulaUpdateDto dto);
    Task<ResultDto<RhConceptosFormulaDeleteDto>> Delete(RhConceptosFormulaDeleteDto dto);
    Task<ResultDto<RhConceptosFormulaResponseDto>> GetByCodigo(RhConceptosFormulaFilterDto dto);
    Task<ResultDto<List<RhConceptosFormulaResponseDto>>> GetAllByConcepto(RhConceptosFormulaFilterDto dto);
    Task<ResultDto<List<RhConceptosFormulaResponseDto>>> GetAll();
    
}