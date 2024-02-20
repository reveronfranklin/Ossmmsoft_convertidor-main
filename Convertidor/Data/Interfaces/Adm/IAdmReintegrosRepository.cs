using Convertidor.Data.Entities.ADM;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmReintegrosRepository
    {
        Task<ADM_REINTEGROS> GetCodigoReintegro(int codigoReintegro);
        Task<List<ADM_REINTEGROS>> GetAll();
        Task<ResultDto<ADM_REINTEGROS>> Add(ADM_REINTEGROS entity);
        Task<ResultDto<ADM_REINTEGROS>> Update(ADM_REINTEGROS entity);
        Task<string> Delete(int codigoReintegro);
        Task<int> GetNextKey();
    }
}
