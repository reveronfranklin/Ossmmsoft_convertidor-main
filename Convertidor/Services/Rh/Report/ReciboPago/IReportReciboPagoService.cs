namespace Convertidor.Services.Rh.Report.Example;

public interface IReportReciboPagoService
{
    Task<ResultDto<byte[]>> GeneratePdf(FilterRepoteNomina filter);
}
