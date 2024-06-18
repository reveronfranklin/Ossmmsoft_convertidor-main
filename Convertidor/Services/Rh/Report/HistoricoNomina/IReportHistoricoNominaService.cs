namespace Convertidor.Services.Rh.Report.HistoricoNomina;

public interface IReportHistoricoNominaService
{
    Task<string> GeneratePdf(FilterRepoteNomina filter);
}