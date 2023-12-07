using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
    public interface IRhTmpRetencionesIncesService
    {
        Task<ResultDto<List<RhTmpRetencionesIncesDto>>> GetRetencionesInces(FilterRetencionesDto filter);
    }
}
