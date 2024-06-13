using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmPucSolCompromisoService
    {
        Task<AdmPucSolCompromisoResponseDto> GetbyCodigoPucSolicitud(int codigoPucSolicitud);
        Task<List<AdmPucSolCompromisoResponseDto>> GetAllbyCodigoPucSolcicitud(int codigoPucSolicitud);
        Task<ResultDto<List<AdmPucSolCompromisoResponseDto>>> GetAll();
        Task<ResultDto<AdmPucSolCompromisoResponseDto>> Update(AdmPucSolCompromisoUpdateDto dto);
        Task<ResultDto<AdmPucSolCompromisoResponseDto>> Create(AdmPucSolCompromisoUpdateDto dto);
        Task<ResultDto<AdmPucSolCompromisoDeleteDto>> Delete(AdmPucSolCompromisoDeleteDto dto);
        
    }
}
