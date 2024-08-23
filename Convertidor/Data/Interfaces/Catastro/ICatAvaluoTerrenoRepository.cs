using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatAvaluoTerrenoRepository
    {
        Task<List<CAT_AVALUO_TERRENO>> GetAll();
    }
}
