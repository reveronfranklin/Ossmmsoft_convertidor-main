using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos.Bm;

namespace Convertidor.Services.Bm;

public interface IBmConteoDetalleService
{
    Task<ResultDto<List<BmConteoDetalleResponseDto>>> GetAll();
    Task<ResultDto<List<BmConteoDetalleResponseDto>>> GetAllByConteo(BmConteoFilterDto filter);
    Task<ResultDto<List<BmConteoDetalleResponseDto>>> Update(BmConteoDetalleUpdateDto dto);
    Task<ResultDto<List<BmConteoDetalleResumenResponseDto>>> GetResumen(int codigoConteo);

    Task<ResultDto<List<BmConteoDetalleResponseDto>>> CreaDetalleConteoDesdeBm1(int codigoConteo, int cantidadConteo,
        List<ICPGetDto> listIcpSeleccionado, int codigoResponsable);

    Task<bool> ConteoIniciado(int codigoConteo);
    Task<bool> DeleteRangeConteo(int codigoConteo);
    Task<bool> ConteoIniciadoConDiferenciaSinComentario(int codigoConteo);
    Task<List<BM_CONTEO_DETALLE>> GetByCodigoConteo(int codigoConteo);
    Task<ResultDto<List<BmConteoDetalleResponseDto>>> ComparaConteo(BmConteoFilterDto filter);
}