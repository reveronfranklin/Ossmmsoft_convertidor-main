using Convertidor.Dtos.Adm;
using Convertidor.Dtos;

namespace Convertidor.Services.Adm
{
    public interface IAdmImpuestosDocumentosOpService
    {
        Task<ResultDto<AdmImpuestosDocumentosOpResponseDto>> Update(AdmImpuestosDocumentosOpUpdateDto dto);
        Task<ResultDto<AdmImpuestosDocumentosOpResponseDto>> Create(AdmImpuestosDocumentosOpUpdateDto dto);
        Task<ResultDto<AdmImpuestosDocumentosOpDeleteDto>> Delete(AdmImpuestosDocumentosOpDeleteDto dto);
    }
}
