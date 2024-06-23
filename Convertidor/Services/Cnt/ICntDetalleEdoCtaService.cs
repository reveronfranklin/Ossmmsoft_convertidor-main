using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntDetalleEdoCtaService
    {
        Task<ResultDto<List<CntDetalleEdoCtaResponseDto>>> GetAll();
        Task<ResultDto<List<CntDetalleEdoCtaResponseDto>>> GetAllByCodigoEstadoCuenta(int codigoEstadoCuenta);
        Task<ResultDto<CntDetalleEdoCtaResponseDto>> Create(CntDetalleEdoCtaUpdateDto dto);
        Task<ResultDto<CntDetalleEdoCtaResponseDto>> Update(CntDetalleEdoCtaUpdateDto dto);
        Task<ResultDto<CntDetalleEdoCtaDeleteDto>> Delete(CntDetalleEdoCtaDeleteDto dto);
    }
}
