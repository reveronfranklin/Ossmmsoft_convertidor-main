namespace Convertidor.Services.Rh.Report.Example;

public interface IReportHistoricoNominaService
{
    Task<string> GeneratePdf(FilterRepoteNomina filter);
}