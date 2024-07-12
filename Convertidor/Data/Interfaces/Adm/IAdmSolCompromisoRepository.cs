using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmSolCompromisoRepository
    {
        Task<List<ADM_SOL_COMPROMISO>> GetAll();
    }
}
