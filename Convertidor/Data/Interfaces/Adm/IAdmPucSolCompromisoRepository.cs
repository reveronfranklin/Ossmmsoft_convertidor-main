using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmPucSolCompromisoRepository
    {
        Task<ADM_PUC_SOL_COMPROMISO> GetbyCodigoPucSolicitud(int codigoPucSolicitud);
        Task<List<ADM_PUC_SOL_COMPROMISO>> GetAllbyCodigoPucSolicitud(int codigoPucSolicitud);
        Task<List<ADM_PUC_SOL_COMPROMISO>> GetAll();
        Task<ResultDto<ADM_PUC_SOL_COMPROMISO>> Add(ADM_PUC_SOL_COMPROMISO entity);
        Task<ResultDto<ADM_PUC_SOL_COMPROMISO>> Update(ADM_PUC_SOL_COMPROMISO entity);
        Task<string> Delete(int codigoPucSolicitud);
        Task<int> GetNextKey();
       
    }
}
