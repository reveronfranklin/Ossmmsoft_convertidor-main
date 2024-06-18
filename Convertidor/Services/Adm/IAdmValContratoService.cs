using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmValContratoService
    {
        Task<ResultDto<List<AdmValContratoResponseDto>>> GetAll();
        Task<ResultDto<AdmValContratoResponseDto>> Update(AdmValContratoUpdateDto dto);
        Task<ResultDto<AdmValContratoResponseDto>> Create(AdmValContratoUpdateDto dto);
        Task<ResultDto<AdmValContratoDeleteDto>> Delete(AdmValContratoDeleteDto dto);


    }
}
