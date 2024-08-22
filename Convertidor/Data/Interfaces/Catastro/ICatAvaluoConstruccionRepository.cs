using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatAvaluoConstruccionRepository
    {
        Task<List<CAT_AVALUO_CONSTRUCCION>> GetAll();
        Task<ResultDto<CAT_AVALUO_CONSTRUCCION>> Add(CAT_AVALUO_CONSTRUCCION entity);
        Task<int> GetNextKey();
    }
}
