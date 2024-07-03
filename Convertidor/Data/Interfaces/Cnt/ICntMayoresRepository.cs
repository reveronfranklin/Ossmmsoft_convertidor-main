using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntMayoresRepository
    {
        Task<List<CNT_MAYORES>> GetAll();
        Task<CNT_MAYORES> GetByCodigo(int codigoMayor);
        Task<ResultDto<CNT_MAYORES>> Add(CNT_MAYORES entity);
        Task<ResultDto<CNT_MAYORES>> Update(CNT_MAYORES entity);
        Task<string> Delete(int codigoMayor);
        Task<int> GetNextKey();
    }
}
