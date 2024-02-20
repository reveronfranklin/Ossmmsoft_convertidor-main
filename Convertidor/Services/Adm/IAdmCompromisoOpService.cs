using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmCompromisoOpService
    {
        Task<ResultDto<AdmCompromisoOpResponseDto>> Update(AdmCompromisoOpUpdateDto dto);
        Task<ResultDto<AdmCompromisoOpResponseDto>> Create(AdmCompromisoOpUpdateDto dto);
        Task<ResultDto<AdmCompromisoOpDeleteDto>> Delete(AdmCompromisoOpDeleteDto dto);


    }
}
