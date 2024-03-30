
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
namespace Convertidor.Services.Rh.Report.Example;

public class ReportExampleService:IReportExampleService
{
    private readonly IConfiguration _configuration;
    public ReportExampleService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void GeneratePdf()
    {
        var settings = _configuration.GetSection("Settings").Get<Settings>();
        
        var pathLogo = @settings.BmFiles + "LogoIzquierda.jpeg";
        var filePath = "invoice.pdf";

        var model = InvoiceDocumentDataSource.GetInvoiceDetails();
        var document = new InvoiceDocument(model,pathLogo);
        document.GeneratePdf(filePath);
    }
    
    
}