using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmPucContratoService
    {
        Task<ResultDto<List<AdmPucContratoResponseDto>>> GetAll();
        Task<ResultDto<AdmPucContratoResponseDto>> Update(AdmPucContratoUpdateDto dto);
        Task<ResultDto<AdmPucContratoResponseDto>> Create(AdmPucContratoUpdateDto dto);
        Task<ResultDto<AdmPucContratoDeleteDto>> Delete(AdmPucContratoDeleteDto dto);


    }
}
