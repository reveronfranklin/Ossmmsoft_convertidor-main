using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmCompromisoOpRepository
    {
        Task<ADM_COMPROMISO_OP> GetCodigoCompromisoOp(int codigoCompromisoOp);
        Task<List<ADM_COMPROMISO_OP>> GetAll();
        Task<ResultDto<ADM_COMPROMISO_OP>> Add(ADM_COMPROMISO_OP entity);
        Task<ResultDto<ADM_COMPROMISO_OP>> Update(ADM_COMPROMISO_OP entity);
        Task<string> Delete(int codigoCompromisoOp);
        Task<int> GetNextKey();
        Task<List<ADM_COMPROMISO_OP>> GetCodigoOrdenPago(int codigoOrdenPago);
    }
}
