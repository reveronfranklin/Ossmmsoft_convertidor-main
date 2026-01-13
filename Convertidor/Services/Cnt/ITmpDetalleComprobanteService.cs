using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ITmpDetalleComprobanteService
    {
        Task<ResultDto<List<TmpDetalleComprobanteResponseDto>>> GetAll();
        Task<ResultDto<TmpDetalleComprobanteResponseDto>> Create(TmpDetalleComprobanteUpdateDto dto);
        Task<ResultDto<TmpDetalleComprobanteResponseDto>> Update(TmpDetalleComprobanteUpdateDto dto);
        Task<ResultDto<TmpDetalleComprobanteDeleteDto>> Delete(TmpDetalleComprobanteDeleteDto dto);
    }
}
