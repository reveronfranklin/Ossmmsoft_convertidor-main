using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmContratosService
    {
        Task<ResultDto<List<AdmContratosResponseDto>>> GetAll();
        Task<ResultDto<AdmContratosResponseDto>> Update(AdmContratosUpdateDto dto);
        Task<ResultDto<AdmContratosResponseDto>> Create(AdmContratosUpdateDto dto);
        Task<ResultDto<AdmContratosDeleteDto>> Delete(AdmContratosDeleteDto dto);


    }
}
