namespace Convertidor.Services.Rh;

public interface IRhConceptosAcumuladoService
{
    Task<ResultDto<RhConceptosAcumulaResponseDto?>> Update(RhConceptosAcumulaUpdateDto dto);
    Task<ResultDto<RhConceptosAcumulaResponseDto>> Create(RhConceptosAcumulaUpdateDto dto);
    Task<ResultDto<RhConceptosAcumulaDeleteDto>> Delete(RhConceptosAcumulaDeleteDto dto);
    Task<ResultDto<RhConceptosAcumulaResponseDto>> GetByCodigo(RhConceptosAcumulaFilterDto dto);
    Task<ResultDto<List<RhConceptosAcumulaResponseDto>>> GetAll();
    Task<ResultDto<List<RhConceptosAcumulaResponseDto>>> GetAllByConcepto(RhConceptosAcumulaFilterDto dto);
}