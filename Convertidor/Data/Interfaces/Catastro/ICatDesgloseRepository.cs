using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatDesgloseRepository
    {
        Task<List<CAT_DESGLOSE>> GetAll();
        Task<CAT_DESGLOSE> GetByCodigo(int codigoDesglose);
        Task<ResultDto<CAT_DESGLOSE>> Add(CAT_DESGLOSE entity);
        Task<ResultDto<CAT_DESGLOSE>> Update(CAT_DESGLOSE entity);
        Task<int> GetNextKey();
    }
}
