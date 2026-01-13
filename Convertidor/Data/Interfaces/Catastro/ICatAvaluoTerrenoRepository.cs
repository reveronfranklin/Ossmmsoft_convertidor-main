using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatAvaluoTerrenoRepository
    {
        Task<List<CAT_AVALUO_TERRENO>> GetAll();
        Task<ResultDto<CAT_AVALUO_TERRENO>> Add(CAT_AVALUO_TERRENO entity);
        Task<CAT_AVALUO_TERRENO> GetByCodigo(int codigoAvaluoTerreno);
        Task<ResultDto<CAT_AVALUO_TERRENO>> Update(CAT_AVALUO_TERRENO entity);
        Task<string> Delete(int codigoAvaluoTerreno);
        Task<int> GetNextKey();
    }
}
