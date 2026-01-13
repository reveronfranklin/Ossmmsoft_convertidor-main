using Convertidor.Dtos.Adm.Pagos;

namespace Convertidor.Services.Adm;

public interface IAdmPagosNotasTercerosService
{
    Task<ResultDto<List<PagoTercerosReportDto>>> GetByLotePago(PagoTerceroFilterDto  filterDto);
}