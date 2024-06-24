using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntDetalleLibroRepository
    {

        Task<List<CNT_DETALLE_LIBRO>> GetAll();
       
    }
}
