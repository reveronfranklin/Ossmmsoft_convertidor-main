using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Rh.Report.Example;

public interface IReportPreResumenSaldoService
{
    Task<ResultDto<string>> GeneratePdf(FilterResumenSaldo filter);
}