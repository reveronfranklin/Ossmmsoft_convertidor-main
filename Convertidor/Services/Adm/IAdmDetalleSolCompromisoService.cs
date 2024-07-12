using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmDetalleSolCompromisoService
    {
        Task<ResultDto<List<AdmDetalleSolCompromisoResponseDto>>> GetAll();
    }
}
