using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh;

public interface IRhMovNominaService
{
    Task<ResultDto<RhMovNominaResponseDto?>> Update(RhMovNominaUpdateDto dto);
    Task<ResultDto<RhMovNominaResponseDto>> Create(RhMovNominaUpdateDto dto);
    Task<ResultDto<RhMovNominaDeleteDto>> Delete(RhMovNominaDeleteDto dto);
    Task<ResultDto<RhMovNominaResponseDto>> GetByCodigo(RhMovNominaFilterDto dto);
    Task<ResultDto<List<RhMovNominaResponseDto>>> GetAllByPersona(RhMovNominaFilterDto dto);
    Task<ResultDto<List<RhMovNominaResponseDto>>> GetAll();
    Task<ResultDto<RhMovNominaResponseDto>> GetByTipoNominaPersonaConcepto(RhMovNominaFilterDto dto);
    Task<ResultDto<RhMovNominaResponseDto?>> UpdateCalculo(int codigoMovNomina, int calculoId,decimal monto);

}