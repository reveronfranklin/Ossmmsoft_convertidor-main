using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntDetalleEdoCtaService
    {
        Task<ResultDto<List<CntDetalleEdoCtaResponseDto>>> GetAll();
    }
}
