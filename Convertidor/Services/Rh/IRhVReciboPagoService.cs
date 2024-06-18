

namespace Convertidor.Services.Rh
{
	public interface IRhVReciboPagoService
    {
        Task<List<RhVReciboPagoResponseDto>> GetAll();
        Task<ResultDto<List<RhVReciboPagoResponseDto>>> GetByFilter(FilterReciboPagoDto dto);
        Task CreateReportReciboPago(int codigoPeriodo, int codigoTipoNomina, int codigoPersona);
    }
}

