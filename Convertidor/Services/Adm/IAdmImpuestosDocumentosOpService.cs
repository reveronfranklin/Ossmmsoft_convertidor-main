using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmImpuestosDocumentosOpService
    {
        Task<ResultDto<AdmImpuestosDocumentosOpResponseDto>> Update(AdmImpuestosDocumentosOpUpdateDto dto);
        Task<ResultDto<AdmImpuestosDocumentosOpResponseDto>> Create(AdmImpuestosDocumentosOpUpdateDto dto);

        Task<ResultDto<List<AdmImpuestosDocumentosOpResponseDto>>> GetByDocumento(AdmImpuestosDocumentosOpFilterDto dto);
        Task<ResultDto<AdmImpuestosDocumentosOpDeleteDto>> Delete(AdmImpuestosDocumentosOpDeleteDto dto);
    }
}
