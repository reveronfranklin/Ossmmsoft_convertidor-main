using Convertidor.Dtos.Adm;
using Convertidor.Dtos;

namespace Convertidor.Services.Adm
{
    public interface IAdmReintegrosService
    {
        Task<ResultDto<AdmReintegrosResponseDto>> Update(AdmReintegrosUpdateDto dto);
        Task<ResultDto<AdmReintegrosResponseDto>> Create(AdmReintegrosUpdateDto dto);
        Task<ResultDto<AdmReintegrosDeleteDto>> Delete(AdmReintegrosDeleteDto dto);
    }
}
