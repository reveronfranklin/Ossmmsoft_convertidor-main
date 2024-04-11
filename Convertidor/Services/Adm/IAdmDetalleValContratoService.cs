using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmDetalleValContratoService
    {
        Task<ResultDto<List<AdmDetalleValContratoResponseDto>>> GetAll();
        Task<ResultDto<AdmDetalleValContratoResponseDto>> Update(AdmDetalleValContratoUpdateDto dto);
        Task<ResultDto<AdmDetalleValContratoResponseDto>> Create(AdmDetalleValContratoUpdateDto dto);
        Task<ResultDto<AdmDetalleValContratoDeleteDto>> Delete(AdmDetalleValContratoDeleteDto dto);


    }
}
