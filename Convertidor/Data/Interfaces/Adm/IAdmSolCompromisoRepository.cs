using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmSolCompromisoRepository
    {
        Task<List<ADM_SOL_COMPROMISO>> GetAll();
        Task<ADM_SOL_COMPROMISO> GetByCodigo(int codigoSolCompromiso);
        Task<ResultDto<ADM_SOL_COMPROMISO>> Add(ADM_SOL_COMPROMISO entity);
        Task<ResultDto<ADM_SOL_COMPROMISO>> Update(ADM_SOL_COMPROMISO entity);
        Task<int> GetNextKey();
    }
}
