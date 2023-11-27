using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
    public interface IRhTmpRetencionesIncesService
    {
        Task<List<RhTmpRetencionesIncesDto>> GetRetencionesInces(FilterRetencionesDto filter);
    }
}
