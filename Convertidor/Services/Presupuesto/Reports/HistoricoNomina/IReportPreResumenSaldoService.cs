using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Rh.Report.Example;

public interface IReportPreResumenSaldoService
{
    Task<string> GeneratePdf(FilterResumenSaldo filter);
}