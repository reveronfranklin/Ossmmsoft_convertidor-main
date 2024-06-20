namespace Convertidor.Services.Rh.Report.Example;

public interface IReportReciboPagoService
{
    Task GeneratePdf(FilterRepoteNomina filter);
}