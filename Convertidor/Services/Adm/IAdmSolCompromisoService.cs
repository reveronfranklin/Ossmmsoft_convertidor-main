using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmSolCompromisoService
    {
        Task<ResultDto<List<AdmSolCompromisoResponseDto>>> GetAll();
    }
}
