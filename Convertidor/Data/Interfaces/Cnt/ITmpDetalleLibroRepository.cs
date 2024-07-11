using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ITmpDetalleLibroRepository
    {
        Task<List<TMP_DETALLE_LIBRO>> GetAll();
    }
}
