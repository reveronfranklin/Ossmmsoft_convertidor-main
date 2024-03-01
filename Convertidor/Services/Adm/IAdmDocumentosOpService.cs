using Convertidor.Dtos.Adm;
using Convertidor.Dtos;

namespace Convertidor.Services.Adm
{
    public interface IAdmDocumentosOpService
    {
        Task<ResultDto<List<AdmDocumentosOpResponseDto>>> GetAll();
        Task<ResultDto<AdmDocumentosOpResponseDto>> Update(AdmDocumentosOpUpdateDto dto);
        Task<ResultDto<AdmDocumentosOpResponseDto>> Create(AdmDocumentosOpUpdateDto dto);
        Task<ResultDto<AdmDocumentosOpDeleteDto>> Delete(AdmDocumentosOpDeleteDto dto);
    }
}
