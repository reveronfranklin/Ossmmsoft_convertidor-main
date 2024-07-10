using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntRelacionDocumentosService
    {
        Task<ResultDto<List<CntRelacionDocumentosResponseDto>>> GetAll();
        Task<ResultDto<CntRelacionDocumentosResponseDto>> Create(CntRelacionDocumentosUpdateDto dto);
        Task<ResultDto<CntRelacionDocumentosResponseDto>> Update(CntRelacionDocumentosUpdateDto dto);
    }
}
