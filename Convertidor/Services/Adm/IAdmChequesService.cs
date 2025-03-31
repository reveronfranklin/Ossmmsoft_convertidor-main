using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmChequesService
    {
  
        Task<ResultDto<AdmChequesResponseDto>> Update(AdmChequesUpdateDto dto);
        Task<ResultDto<AdmChequesResponseDto>> Create(AdmChequesUpdateDto dto);
        Task<ResultDto<AdmChequesDeleteDto>> Delete(AdmChequesDeleteDto dto);
        Task<ResultDto<List<AdmChequesResponseDto>>> GetByLote(AdmChequeFilterDto dto);


    }
}
