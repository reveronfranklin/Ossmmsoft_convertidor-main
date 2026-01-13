using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntDetalleComprobanteService
    {
        Task<ResultDto<List<CntDetalleComprobanteResponseDto>>> GetAll();
        Task<ResultDto<CntDetalleComprobanteResponseDto>> GetByCodigo(int codigoDetalleComprobante);
        Task<ResultDto<List<CntDetalleComprobanteResponseDto>>> GetAllByCodigoComprobante(int codigoComprobante);
        Task<ResultDto<CntDetalleComprobanteResponseDto>> Create(CntDetalleComprobanteUpdateDto dto);
        Task<ResultDto<CntDetalleComprobanteResponseDto>> Update(CntDetalleComprobanteUpdateDto dto);
        Task<ResultDto<CntDetalleComprobanteDeleteDto>> Delete(CntDetalleComprobanteDeleteDto dto);
    }
}
