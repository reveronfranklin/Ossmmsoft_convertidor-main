using Xunit;
using Convertidor.Controllers;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;
using Convertidor.Services.Rh.Report.Example;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System.Reflection;
using System.Text;

namespace Convertidor.Tests.Services.Rh;

public class ReciboPagoPdfTests : IDisposable
{
    private readonly Mock<IRhReporteNominaTemporalService> _temporal = new();
    private readonly Mock<IRhReporteNominaHistoricoService> _historico = new();
    private readonly Mock<IRhTipoNominaService> _nomina = new();
    private readonly string _directory = Path.Combine(Path.GetTempPath(), "recibo-test-" + Guid.NewGuid().ToString("N"));
    private readonly string _logo;

    public ReciboPagoPdfTests()
    {
        Directory.CreateDirectory(_directory);
        _logo = Path.Combine(_directory, "logo.png");
        File.WriteAllBytes(_logo, System.Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+a0S0AAAAASUVORK5CYII="));
        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
        _nomina.Setup(x => x.GetByCodigo(It.IsAny<RhTiposNominaFilterDto>()))
            .ReturnsAsync(new RhTiposNominaResponseDto { Descripcion = "NOMINA DE PRUEBA" });
    }

    private ReportReciboPagoService Create(string? logo = null, string empresa = "1")
    {
        var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> {
            ["settings:EmpresaConfig"] = empresa, ["settings:RhReciboLogo"] = logo ?? _logo
        }).Build();
        var env = new Mock<IWebHostEnvironment>();
        env.SetupGet(x => x.ContentRootPath).Returns(_directory);
        return new(config, _historico.Object, _nomina.Object, _temporal.Object, env.Object,
            NullLogger<ReportReciboPagoService>.Instance);
    }

    private static RhReporteNominaResponseDto Row(int persona, int oficina = 10, int empresa = 1) => new() {
        CodigoEmpresa = empresa, CodigoTipoNomina = 20, CodigoPeriodo = 5952,
        CodigoPersona = persona, CodigoIcp = oficina, CodigoIcpConcat = "01-" + oficina,
        Cedula = persona, Nombre = persona == 1 ? "ALFA PRUEBA" : "BETA PRUEBA",
        DenominacionCargo = "ANALISTA", FechaIngreso = new(2020, 1, 1), FechaIngresoString = "01/01/2020",
        FechaNomina = new(2025, 10, 9), FechaNominaString = "09/10/2025", Banco = "BANCO DE PRUEBA",
        NoCuenta = "0000", DenominacionConcepto = "SUELDO BASICO", NumeroConcepto = "022",
        ComplementoConcepto = "Detalle de prueba", Asignacion = 195m, Deduccion = 0m, Porcentaje = "0"
    };
    private static FilterRepoteNomina Filter(int? persona = 1) => new() {
        CodigoTipoNomina = 20, CodigoPeriodo = 5952, CodigoIcp = 10, CodigoPersona = persona
    };
    private static ResultDto<List<RhReporteNominaResponseDto>> Result(params RhReporteNominaResponseDto[] rows) =>
        new(rows.ToList()) { IsValid = true };
    private static string Text(byte[] pdf)
    {
        using var document = new PdfDocument(new PdfReader(new MemoryStream(pdf)));
        return string.Join("\n", Enumerable.Range(1, document.GetNumberOfPages())
            .Select(page => PdfTextExtractor.GetTextFromPage(document.GetPage(page))));
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(false, false)]
    public async Task GeneraPdfRealYRespetaPersonaOficinaEmpresa(bool temporal, bool individual)
    {
        var rows = Result(Row(1), Row(2), Row(9, 20), Row(8, 10, 2));
        _temporal.Setup(x => x.GetByPeriodoTipoNomina(It.IsAny<FilterRepoteNomina>()))
            .ReturnsAsync(temporal ? rows : Result());
        _historico.Setup(x => x.GetByPeriodoTipoNomina(It.IsAny<FilterRepoteNomina>())).ReturnsAsync(rows);
        var filter = Filter(individual ? 1 : null);
        var response = await Create().GeneratePdf(filter);
        Assert.True(response.IsValid, response.Message);
        Assert.Equal("%PDF-", Encoding.ASCII.GetString(response.Data, 0, 5));
        using (var pages = new PdfDocument(new PdfReader(new MemoryStream(response.Data))))
            for (var page = 1; page <= pages.GetNumberOfPages(); page++)
                Assert.Contains("DEPARTAMENTO", PdfTextExtractor.GetTextFromPage(pages.GetPage(page)));
        var text = Text(response.Data);
        Assert.Contains("ALFA PRUEBA", text);
        Assert.Equal(!individual, text.Contains("BETA PRUEBA"));
        Assert.Contains("195,00", text);
        Assert.Equal(temporal && !individual, text.Contains("PROVISIONAL"));
        Assert.False(filter.ImprimirMarcaAgua); // No mutar el filtro recibido.
        Assert.Empty(Directory.GetFiles(_directory, "*.pdf"));
        var output = Environment.GetEnvironmentVariable("R49_PDF_OUTPUT");
        if (!string.IsNullOrWhiteSpace(output)) {
            Directory.CreateDirectory(output);
            await File.WriteAllBytesAsync(Path.Combine(output,
                $"{(individual ? "individual" : "lote")}-{(temporal ? "temporal" : "historico")}.pdf"), response.Data);
        }
    }

    [Fact]
    public async Task SolicitudesConcurrentesNoMezclanRecibos()
    {
        _temporal.Setup(x => x.GetByPeriodoTipoNomina(It.IsAny<FilterRepoteNomina>()))
            .ReturnsAsync(Result(Row(1), Row(2)));
        var service = Create();
        var results = await Task.WhenAll(Task.Run(() => service.GeneratePdf(Filter(1))),
            Task.Run(() => service.GeneratePdf(Filter(2))));
        Assert.All(results, result => Assert.True(result.IsValid, result.Message));
        Assert.Contains("ALFA PRUEBA", Text(results[0].Data));
        Assert.DoesNotContain("BETA PRUEBA", Text(results[0].Data));
        Assert.Contains("BETA PRUEBA", Text(results[1].Data));
        Assert.DoesNotContain("ALFA PRUEBA", Text(results[1].Data));
    }

    [Fact]
    public async Task ErrorTemporalNoSeOcultaConHistorico()
    {
        _temporal.Setup(x => x.GetByPeriodoTipoNomina(It.IsAny<FilterRepoteNomina>()))
            .ReturnsAsync((ResultDto<List<RhReporteNominaResponseDto>>)null!);
        var result = await Create().GeneratePdf(Filter());
        Assert.False(result.IsValid);
        _historico.Verify(x => x.GetByPeriodoTipoNomina(It.IsAny<FilterRepoteNomina>()), Times.Never);
    }

    [Fact]
    public async Task SinCoincidenciasNoEntregaOtroRecibo()
    {
        _temporal.Setup(x => x.GetByPeriodoTipoNomina(It.IsAny<FilterRepoteNomina>())).ReturnsAsync(Result(Row(2)));
        var result = await Create().GeneratePdf(Filter());
        Assert.False(result.IsValid);
        Assert.Contains("No hay recibos", result.Message);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task ValidaFiltrosEmpresaYNoEnviaCorreo()
    {
        Assert.False((await Create().GeneratePdf(new FilterRepoteNomina())).IsValid);
        Assert.False((await Create(empresa: "0").GeneratePdf(Filter())).IsValid);
        var filter = Filter(); filter.SendEmail = true;
        Assert.Contains("correo", (await Create().GeneratePdf(filter)).Message);
        _temporal.Verify(x => x.GetByPeriodoTipoNomina(It.IsAny<FilterRepoteNomina>()), Times.Never);
    }

    [Fact]
    public async Task LogoFaltanteProduceErrorControlado()
    {
        _temporal.Setup(x => x.GetByPeriodoTipoNomina(It.IsAny<FilterRepoteNomina>())).ReturnsAsync(Result(Row(1)));
        var result = await Create(Path.Combine(_directory, "ausente.png")).GeneratePdf(Filter());
        Assert.False(result.IsValid);
        Assert.Contains("logo", result.Message);
    }

    [Fact]
    public async Task EndpointEntregaBytesInlineYRequiereToken()
    {
        var pdf = Encoding.ASCII.GetBytes("%PDF-test");
        var generator = new Mock<IReportReciboPagoService>();
        generator.Setup(x => x.GeneratePdf(It.IsAny<FilterRepoteNomina>()))
            .ReturnsAsync(new ResultDto<byte[]>(pdf) { IsValid = true });
        var controller = new RhVReciboPagoController(null!, generator.Object) {
            ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
        };
        var result = Assert.IsType<FileContentResult>(await controller.GeneratePdf(Filter()));
        Assert.Equal(pdf, result.FileContents);
        Assert.Equal("application/pdf", result.ContentType);
        Assert.Equal("no-store", controller.Response.Headers["Cache-Control"]);
        Assert.Contains("inline", controller.Response.Headers["Content-Disposition"].ToString());
        Assert.NotNull(typeof(RhVReciboPagoController).GetMethod("GeneratePdf")!.GetCustomAttribute<AuthorizeAttribute>());
    }

    public void Dispose() => Directory.Delete(_directory, true);
}
