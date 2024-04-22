using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmBeneficariosOpService
    {
        Task<ResultDto<List<AdmBeneficiariosOpResponseDto>>> GetAll();
        Task<ResultDto<AdmBeneficiariosOpResponseDto>> Update(AdmBeneficiariosOpUpdateDto dto);
        Task<ResultDto<AdmBeneficiariosOpResponseDto>> Create(AdmBeneficiariosOpUpdateDto dto);
        Task<ResultDto<AdmBeneficiariosOpDeleteDto>> Delete(AdmBeneficiariosOpDeleteDto dto);
    }
}
