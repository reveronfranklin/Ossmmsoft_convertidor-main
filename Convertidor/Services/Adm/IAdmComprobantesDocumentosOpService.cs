using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmComprobantesDocumentosOpService
    {
        Task<ResultDto<List<AdmComprobantesDocumentosOpResponseDto>>> GetAll();
        Task<ResultDto<AdmComprobantesDocumentosOpResponseDto>> Update(AdmComprobantesDocumentosOpUpdateDto dto);
        Task<ResultDto<AdmComprobantesDocumentosOpResponseDto>> Create(AdmComprobantesDocumentosOpUpdateDto dto);
        Task<ResultDto<AdmComprobantesDocumentosOpDeleteDto>> Delete(AdmComprobantesDocumentosOpDeleteDto dto);
    }
}
