using Convertidor.Dtos.Adm;
using Convertidor.Dtos;

namespace Convertidor.Services.Adm
{
    public interface IAdmImpuestosOpService
    {
        Task<ResultDto<AdmImpuestosOpResponseDto>> Update(AdmImpuestosOpUpdateDto dto);
        Task<ResultDto<AdmImpuestosOpResponseDto>> Create(AdmImpuestosOpUpdateDto dto);
        Task<ResultDto<AdmImpuestosOpDeleteDto>> Delete(AdmImpuestosOpDeleteDto dto);
    }
}
