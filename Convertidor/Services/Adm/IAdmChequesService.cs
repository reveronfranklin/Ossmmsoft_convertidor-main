using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmChequesService
    {
        Task<ResultDto<List<AdmChequesResponseDto>>> GetAll();
        Task<ResultDto<AdmChequesResponseDto>> Update(AdmChequesUpdateDto dto);
        Task<ResultDto<AdmChequesResponseDto>> Create(AdmChequesUpdateDto dto);
        Task<ResultDto<AdmChequesDeleteDto>> Delete(AdmChequesDeleteDto dto);


    }
}
