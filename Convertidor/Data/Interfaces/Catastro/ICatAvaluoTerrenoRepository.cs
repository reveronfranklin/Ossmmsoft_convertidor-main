using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatAvaluoTerrenoRepository
    {
        Task<List<CAT_AVALUO_TERRENO>> GetAll();
        Task<ResultDto<CAT_AVALUO_TERRENO>> Add(CAT_AVALUO_TERRENO entity);
        Task<int> GetNextKey();
    }
}
