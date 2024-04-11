namespace Convertidor.Services.Rh.Report.Example;

public interface IReportHistoricoNominaService
{
    Task GeneratePdf(FilterRepoteNomina filter);
}