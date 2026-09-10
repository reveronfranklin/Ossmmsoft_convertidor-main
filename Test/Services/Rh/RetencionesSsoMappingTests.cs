using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Repository.Rh;
using Xunit;

namespace Convertidor.Tests.Services.Rh;

public class RetencionesSsoMappingTests
{
    private readonly RhTmpRetencionesSsoService _temporal = new(null!, null!, null!, null!);
    private readonly RhHRetencionesSsoService _historico = new(null!, null!);

    [Theory]
    [InlineData(5.25, 21.00)]
    [InlineData(0, 0)]
    [InlineData(-5.25, -21.00)]
    public async Task MapeosConservanRpeYSso(decimal trabajador, decimal patrono)
    {
        var entity = new RH_TMP_RETENCIONES_SSO
        {
            MONTO_RPE_TRABAJADOR = trabajador,
            MONTO_RPE_PATRONO = patrono,
            MONTO_SSO_TRABAJADOR = 40m,
            MONTO_SSO_PATRONO = 100m,
            MONTO_TOTAL_RETENCION = -165m,
            CEDULATEXTO = "V123",
            FECHA_NOMINA = "202609"
        };
        var individual = await _temporal.MapRhTmpRetencionesSsoDto(entity);
        var lista = await _temporal.MapListRhTmpRetencionesSsoDto(new() { entity });
        var historicos = _temporal.MapRetencionesSsoTmpH(new() { entity });
        var historico = await _historico.MapListRhHRetencionesSsoDto(historicos);

        foreach (var dto in new[] { individual, Assert.Single(lista), Assert.Single(historico) })
        {
            Assert.Equal(trabajador, dto.MontoRpeTrabajador);
            Assert.Equal(patrono, dto.MontoRpePatrono);
            Assert.Equal(40m, dto.MontoSsoTrabajador);
            Assert.Equal(100m, dto.MontoSsoPatrono);
            Assert.Equal(-165m, dto.MontoTotalRetencion);
            Assert.Equal("V123", dto.CedulaTexto);
            Assert.Equal("202609", dto.FechaNomina);
        }
    }

    [Fact]
    public async Task AgrupacionDistingueCadaRpeSinSumarDuplicados()
    {
        var original = new RH_TMP_RETENCIONES_SSO { MONTO_RPE_TRABAJADOR = 5m, MONTO_RPE_PATRONO = 20m };
        var diferenteTrabajador = new RH_TMP_RETENCIONES_SSO { MONTO_RPE_TRABAJADOR = 6m, MONTO_RPE_PATRONO = 20m };
        var diferentePatrono = new RH_TMP_RETENCIONES_SSO { MONTO_RPE_TRABAJADOR = 5m, MONTO_RPE_PATRONO = 21m };
        var result = await _temporal.MapListRhTmpRetencionesSsoDto(new()
        {
            original, original, diferenteTrabajador, diferentePatrono
        });
        Assert.Equal(3, result.Count);
        Assert.Contains(result, x => x.MontoRpeTrabajador == 5m && x.MontoRpePatrono == 20m);
        Assert.Contains(result, x => x.MontoRpeTrabajador == 6m && x.MontoRpePatrono == 20m);
        Assert.Contains(result, x => x.MontoRpeTrabajador == 5m && x.MontoRpePatrono == 21m);
    }

    [Fact]
    public async Task ListasVaciasPermanecenVacias()
    {
        Assert.Empty(await _temporal.MapListRhTmpRetencionesSsoDto(new()));
        Assert.Empty(await _historico.MapListRhHRetencionesSsoDto(new()));
    }
}
