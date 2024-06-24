using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntDetalleLibroRepository
    {

        Task<List<CNT_DETALLE_LIBRO>> GetAll();
        Task<List<CNT_DETALLE_LIBRO>> GetByCodigoLibro(int codigoLibro);
        Task<ResultDto<CNT_DETALLE_LIBRO>> Add(CNT_DETALLE_LIBRO entity);
        Task<int> GetNextKey();
    }
}
