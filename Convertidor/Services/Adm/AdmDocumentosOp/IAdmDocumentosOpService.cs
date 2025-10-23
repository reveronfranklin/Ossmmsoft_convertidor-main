using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmDocumentosOp
{
    public interface IAdmDocumentosOpService
    {
        Task<ResultDto<List<AdmDocumentosOpResponseDto>>> GetAll();
        Task<ResultDto<List<AdmDocumentosOpResponseDto>>> GetByCodigoOrdenPago(AdmDocumentosFilterDto dto);
        Task<ResultDto<AdmDocumentosOpResponseDto>> Update(AdmDocumentosOpUpdateDto dto);
        Task<ResultDto<AdmDocumentosOpResponseDto>> Create(AdmDocumentosOpUpdateDto dto);
        Task<ResultDto<AdmDocumentosOpDeleteDto>> Delete(AdmDocumentosOpDeleteDto dto);
        Task<decimal> GetBaseImponibleByCodigoOrdenPago(int codigoOrdenPago);
    }
}
