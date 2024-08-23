using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatAvaluoConstruccionRepository
    {
        Task<List<CAT_AVALUO_CONSTRUCCION>> GetAll();
        Task<CAT_AVALUO_CONSTRUCCION> GetByCodigo(int codigoAvaluoConstruccion);
        Task<ResultDto<CAT_AVALUO_CONSTRUCCION>> Add(CAT_AVALUO_CONSTRUCCION entity);
        Task<ResultDto<CAT_AVALUO_CONSTRUCCION>> Update(CAT_AVALUO_CONSTRUCCION entity);
        Task<int> GetNextKey();
    }
}
