using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ITmpDetalleLibroRepository
    {
        Task<List<TMP_DETALLE_LIBRO>> GetAll();
        Task<ResultDto<TMP_DETALLE_LIBRO>> Add(TMP_DETALLE_LIBRO entity);
        Task<int> GetNextKey();
    }
}
