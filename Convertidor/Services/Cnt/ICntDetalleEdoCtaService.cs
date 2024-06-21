using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntDetalleEdoCtaService
    {
        Task<ResultDto<List<CntDetalleEdoCtaResponseDto>>> GetAll();
        Task<ResultDto<List<CntDetalleEdoCtaResponseDto>>> GetAllByCodigoEstadoCuenta(int codigoEstadoCuenta);
    }
}
