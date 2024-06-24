using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntDetalleLibroService
    {
        Task<ResultDto<List<CntDetalleLibroResponseDto>>> GetAll();
    }
}
