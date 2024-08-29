using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatDesgloseRepository
    {
        Task<List<CAT_DESGLOSE>> GetAll();

        Task<ResultDto<CAT_DESGLOSE>> Add(CAT_DESGLOSE entity);
        Task<int> GetNextKey();
    }
}
