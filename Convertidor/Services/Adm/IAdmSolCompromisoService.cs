using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmSolCompromisoService
    {
        Task<ResultDto<List<AdmSolCompromisoResponseDto>>> GetAll();
        Task<ResultDto<AdmSolCompromisoResponseDto>> Create(AdmSolCompromisoUpdateDto dto);
        Task<ResultDto<AdmSolCompromisoResponseDto>> Update(AdmSolCompromisoUpdateDto dto);
        Task<ResultDto<AdmSolCompromisoDeleteDto>> Delete(AdmSolCompromisoDeleteDto dto);
    }
}
