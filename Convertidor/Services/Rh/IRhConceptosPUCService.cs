using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh;

public interface IRhConceptosPUCService
{
    Task<ResultDto<RhConceptosPUCResponseDto?>> Update(RhConceptosPUCUpdateDto dto);
    Task<ResultDto<RhConceptosPUCResponseDto>> Create(RhConceptosPUCUpdateDto dto);
    Task<ResultDto<RhConceptosPUCDeleteDto>> Delete(RhConceptosPUCDeleteDto dto);
    Task<ResultDto<RhConceptosPUCResponseDto>> GetByCodigo(RhConceptosPUCFilterDto dto);
    Task<ResultDto<List<RhConceptosPUCResponseDto>>> GetAllByConcepto(RhConceptosPUCFilterDto dto);
    Task<ResultDto<List<RhConceptosPUCResponseDto>>> GetAll();
    
}