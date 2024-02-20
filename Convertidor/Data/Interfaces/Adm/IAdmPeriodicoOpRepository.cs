using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos;
using System.Threading.Tasks;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmPeriodicoOpRepository
    {
        Task<List<ADM_PERIODICO_OP>> GetAll();
        Task<ADM_PERIODICO_OP> GetCodigoPeriodicoOp(int codigoPeriodicoOp);
        Task<ADM_PERIODICO_OP> GetFechaPago(DateTime fechaPago);
        Task<ResultDto<ADM_PERIODICO_OP>> Add(ADM_PERIODICO_OP entity);
        Task<ResultDto<ADM_PERIODICO_OP>> Update(ADM_PERIODICO_OP entity);
        Task<string> Delete(int codigoPeriodicoOp);
        Task<int> GetNextKey();
    }
}
