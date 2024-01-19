using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis;

public interface IOssCalculoService
{
    Task<ResultDto<OssCalculoResponseDto?>> Update(OssCalculoUpdateDto dto);
    Task<ResultDto<OssCalculoResponseDto>> Create(OssCalculoUpdateDto dto, int idCalculo);
    Task<ResultDto<OssCalculoDeleteDto>> Delete(OssCalculoDeleteDto dto);
    Task<ResultDto<OssCalculoResponseDto>> GetById(OssCalculoFilterDto dto);
  
    Task<ResultDto<List<OssCalculoResponseDto>>> GetByIdCalculo(int idCalculo);
    Task<int> GetNextCalculo();
    Task<ResultDto<List<OssCalculoResponseDto>>> CalculoTipoNominaPersonaConcepto(RhMovNominaFilterDto dto);
    Task<decimal> GetTotalByIdCalculo(int idCalculo);
}