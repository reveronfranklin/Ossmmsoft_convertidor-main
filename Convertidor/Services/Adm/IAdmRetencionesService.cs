using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm;

public interface IAdmRetencionesService
{
    Task<ResultDto<List<AdmRetencionesResponseDto>>> GetAll();
    Task<ResultDto<AdmRetencionesResponseDto>> Update(AdmRetencionesUpdateDto dto);
    Task<ResultDto<AdmRetencionesResponseDto>> Create(AdmRetencionesUpdateDto dto);
    Task<ResultDto<AdmRetencionesDeleteDto>> Delete(AdmRetencionesDeleteDto dto);

}