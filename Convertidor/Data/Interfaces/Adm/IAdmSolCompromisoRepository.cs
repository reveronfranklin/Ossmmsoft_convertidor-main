using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmSolCompromisoRepository
    {
        Task<List<ADM_SOL_COMPROMISO>> GetAll();
        Task<ResultDto<ADM_SOL_COMPROMISO>> Add(ADM_SOL_COMPROMISO entity);
        Task<int> GetNextKey();
    }
}
