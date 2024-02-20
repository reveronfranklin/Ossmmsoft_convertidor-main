using Convertidor.Dtos.Adm;
using Convertidor.Dtos;

namespace Convertidor.Services.Adm
{
    public interface IAdmRetencionesOpService
    {
        Task<ResultDto<AdmRetencionesOpResponseDto>> Update(AdmRetencionesOpUpdateDto dto);
        Task<ResultDto<AdmRetencionesOpResponseDto>> Create(AdmRetencionesOpUpdateDto dto);
        Task<ResultDto<AdmRetencionesOpDeleteDto>> Delete(AdmRetencionesOpDeleteDto dto);
    }
}
