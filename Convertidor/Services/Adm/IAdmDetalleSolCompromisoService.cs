using Convertidor.Dtos.Adm;
using NuGet.Protocol.Core.Types;

namespace Convertidor.Services.Adm
{
    public interface IAdmDetalleSolCompromisoService
    {
        Task<ResultDto<List<AdmDetalleSolCompromisoResponseDto>>> GetAll();
        Task<List<AdmDetalleSolCompromisoResponseDto>> GetAllByCodigoDetalleSolicitud(int codigoDetalleSolicitud);
        Task<ResultDto<AdmDetalleSolCompromisoResponseDto>> Update(AdmDetalleSolCompromisoUpdateDto dto);
        Task<ResultDto<AdmDetalleSolCompromisoResponseDto>> Create(AdmDetalleSolCompromisoUpdateDto dto);
        Task<ResultDto<AdmDetalleSolCompromisoDeleteDto>> Delete(AdmDetalleSolCompromisoDeleteDto dto);
    }
}
