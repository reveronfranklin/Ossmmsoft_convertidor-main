using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmImpuestosOpService
    {
        Task<ResultDto<List<AdmImpuestosOpResponseDto>>> GetAll();
        Task<ResultDto<AdmImpuestosOpResponseDto>> Update(AdmImpuestosOpUpdateDto dto);
        Task<ResultDto<AdmImpuestosOpResponseDto>> Create(AdmImpuestosOpUpdateDto dto);
        Task<ResultDto<AdmImpuestosOpDeleteDto>> Delete(AdmImpuestosOpDeleteDto dto);
    }
}
