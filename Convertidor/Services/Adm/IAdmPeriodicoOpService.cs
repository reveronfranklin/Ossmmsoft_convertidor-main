using Convertidor.Dtos.Adm;
using Convertidor.Dtos;

namespace Convertidor.Services.Adm
{
    public interface IAdmPeriodicoOpService
    {
        Task<ResultDto<AdmPeriodicoOpResponseDto>> Update(AdmPeriodicoOpUpdateDto dto);
        Task<ResultDto<AdmPeriodicoOpResponseDto>> Create(AdmPeriodicoOpUpdateDto dto);
        Task<ResultDto<AdmPeriodicoOpDeleteDto>> Delete(AdmPeriodicoOpDeleteDto dto);
    }
}
