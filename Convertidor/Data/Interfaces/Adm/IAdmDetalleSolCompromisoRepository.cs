using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmDetalleSolCompromisoRepository
    {
        Task<List<ADM_DETALLE_SOL_COMPROMISO>> GetAll();
        Task<ResultDto<ADM_DETALLE_SOL_COMPROMISO>> Add(ADM_DETALLE_SOL_COMPROMISO entity);
        Task<int> GetNextKey();
    }
}
