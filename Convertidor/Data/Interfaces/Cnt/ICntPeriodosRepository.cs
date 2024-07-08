using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntPeriodosRepository
    {
        Task<List<CNT_PERIODOS>> GetAll();
        Task<CNT_PERIODOS> GetByCodigo(int codigoPeriodo);
        Task<ResultDto<CNT_PERIODOS>> Add(CNT_PERIODOS entity);
        Task<ResultDto<CNT_PERIODOS>> Update(CNT_PERIODOS entity);
        Task<string> Delete(int codigoPeriodo);
        Task<int> GetNextKey();
    }
}
