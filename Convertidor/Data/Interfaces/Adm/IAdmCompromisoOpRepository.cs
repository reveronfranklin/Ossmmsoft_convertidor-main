using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmCompromisoOpRepository
    {
        Task<ADM_COMPROMISO_OP> GetCodigoCompromisoOp(int CodigoCompromisoOp);
        Task<List<ADM_COMPROMISO_OP>> GetAll();
        Task<ResultDto<ADM_COMPROMISO_OP>> Add(ADM_COMPROMISO_OP entity);
        Task<ResultDto<ADM_COMPROMISO_OP>> Update(ADM_COMPROMISO_OP entity);
        Task<string> Delete(int CodigoCompromisoOp);
        Task<int> GetNextKey();
    }
}
