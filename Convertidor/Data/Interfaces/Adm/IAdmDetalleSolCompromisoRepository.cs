using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmDetalleSolCompromisoRepository
    {
        Task<List<ADM_DETALLE_SOL_COMPROMISO>> GetAll();
    }
}
