using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatAvaluoConstruccionRepository
    {
        Task<List<CAT_AVALUO_CONSTRUCCION>> GetAll();
    }
}
