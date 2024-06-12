using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmPucSolCompromisoService
    {
        Task<ResultDto<List<AdmPucSolCompromisoResponseDto>>> GetAll();
        Task<ResultDto<AdmPucSolCompromisoResponseDto>> Update(AdmPucSolCompromisoUpdateDto dto);
        Task<ResultDto<AdmPucSolCompromisoResponseDto>> Create(AdmPucSolCompromisoUpdateDto dto);
        Task<ResultDto<AdmPucSolCompromisoDeleteDto>> Delete(AdmPucSolCompromisoDeleteDto dto);
        
    }
}
