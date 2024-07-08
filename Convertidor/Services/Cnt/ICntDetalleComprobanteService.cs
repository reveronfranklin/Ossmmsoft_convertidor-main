using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntDetalleComprobanteService
    {
        Task<ResultDto<List<CntDetalleComprobanteResponseDto>>> GetAll();
    }
}
