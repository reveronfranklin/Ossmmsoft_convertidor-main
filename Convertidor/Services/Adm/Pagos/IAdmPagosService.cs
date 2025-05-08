using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.Pagos;

namespace Convertidor.Services.Adm.Pagos;

public interface IAdmPagosService
{
    Task<ResultDto<List<PagoResponseDto>>> GetByLote(AdmChequeFilterDto dto);
}