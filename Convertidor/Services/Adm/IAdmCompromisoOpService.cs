using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmCompromisoOpService
    {
        Task<ResultDto<List<AdmCompromisoOpResponseDto>>> GetAll();
        Task<ResultDto<AdmCompromisoOpResponseDto>> Update(AdmCompromisoOpUpdateDto dto);
        Task<ResultDto<AdmCompromisoOpResponseDto>> Create(AdmCompromisoOpUpdateDto dto);
        Task<ResultDto<AdmCompromisoOpDeleteDto>> Delete(AdmCompromisoOpDeleteDto dto);
   
        Task<string> GetCompromisosByOrdenPago(int codigoOrdenPago, int codigoPresupuesto);
        Task<ResultDto<List<AdmCompromisoOpResponseDto>>> GetByOrdenPago(int codigoOrdenPago, int codigoPresupuesto);

    }
}
