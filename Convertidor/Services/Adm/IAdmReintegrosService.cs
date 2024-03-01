using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmReintegrosService
    {
        Task<ResultDto<List<AdmReintegrosResponseDto>>> GetAll();
        Task<ResultDto<AdmReintegrosResponseDto>> Update(AdmReintegrosUpdateDto dto);
        Task<ResultDto<AdmReintegrosResponseDto>> Create(AdmReintegrosUpdateDto dto);
        Task<ResultDto<AdmReintegrosDeleteDto>> Delete(AdmReintegrosDeleteDto dto);
    }
}
