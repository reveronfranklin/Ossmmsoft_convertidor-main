using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm;

public interface IAdmOrdenesPagoPorPagarService
{
    Task<ResultDto<List<AdmOrdenPagoPendientePagoDto>>> GetAll(AdmOrdenPagoPendientePagoFilterDto filter);
}