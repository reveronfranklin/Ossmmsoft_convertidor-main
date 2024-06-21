using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntDetalleEdoCtaRepository
    {

        Task<List<CNT_DETALLE_EDO_CTA>> GetAll();
    }
}
