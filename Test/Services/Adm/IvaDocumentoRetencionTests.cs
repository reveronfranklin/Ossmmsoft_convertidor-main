using Convertidor.Data.Entities.Adm;
using Convertidor.Services.Adm.AdmDocumentosOp;
using Convertidor.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Convertidor.Tests.Services.Adm;

public class IvaDocumentoRetencionTests
{
    [Fact]
    public void ConsultaTiposNoLeeAuditoriaNiFkLegacyQuePuedenSerNull()
    {
        using var context = ContextoConsulta();
        var sql = IvaDocumentoConsultas.Tipos(context.ADM_DESCRIPTIVAS.Where(x => x.CODIGO == "IVA"))
            .ToQueryString();
        Assert.DoesNotContain("USUARIO_UPD", sql);
        Assert.DoesNotContain("USUARIO_INS", sql);
        Assert.DoesNotContain("DESCRIPCION_FK_ID", sql);
        Assert.DoesNotContain("CODIGO_EMPRESA", sql);
        Assert.Contains("TITULO_ID", sql);
        Assert.Contains("EXTRA1", sql);
    }

    [Fact]
    public void ConsultaConceptosSoloLeeIdentificadores()
    {
        using var context = ContextoConsulta();
        var sql = IvaDocumentoConsultas.Conceptos(context.ADM_RETENCIONES.Where(x => x.EXTRA1 == "100"))
            .ToQueryString();
        Assert.Contains("CODIGO_RETENCION", sql);
        Assert.Contains("TIPO_RETENCION_ID", sql);
        Assert.DoesNotContain("FECHA_INI", sql);
        Assert.DoesNotContain("USUARIO_UPD", sql);
        Assert.DoesNotContain("TIPO_PERSONA_ID", sql);
    }

    [Fact]
    public void ConsultaDocumentosNoCargaFechasNiDatosOpcionalesAjenosAlCalculo()
    {
        using var context = ContextoConsulta();
        var sql = IvaDocumentoConsultas.Documentos(context.ADM_DOCUMENTOS_OP
            .Where(x => x.CODIGO_ORDEN_PAGO == 18410)).ToQueryString();
        Assert.DoesNotContain("FECHA_COMPROBANTE", sql);
        Assert.DoesNotContain("FECHA_DOCUMENTO", sql);
        Assert.DoesNotContain("FECHA_INS", sql);
        Assert.Contains("MONTO_RETENIDO", sql);
    }

    [Fact]
    public void PayloadOp18410CalculaIvaYRetencionEsperados()
    {
        var calculo = CalculoImpuestoRetencion.Calcular(500m, 0m, 100m, 8m);
        Assert.Equal(462.96m, calculo.BaseImponible);
        Assert.Equal(37.04m, calculo.MontoImpuesto);
        Assert.Equal(37.04m, calculo.MontoRetenido);
    }

    private static DataContextAdm ContextoConsulta() => new(new DbContextOptionsBuilder<DataContextAdm>()
        .UseOracle("User Id=test;Password=test;Data Source=localhost/test",
            oracle => oracle.UseOracleSQLCompatibility("11")).Options);

    [Fact]
    public void SeleccionaTipoOchoAunqueElPrimerTipoDelCatalogoSeaSeis()
    {
        var tipos = new[]
        {
            Tipo(101, "I.V.A. (6% INCLUIDO EN LOS PRECIOS)"),
            Tipo(102, "I.V.A. (8%)"),
            new ADM_DESCRIPTIVAS { DESCRIPCION_ID = 103, CODIGO = "IVA", TITULO_ID = 99, EXTRA1 = "8" }
        };
        var seleccionado = IvaDocumentoRetencion.ResolverTipo(tipos, 1, 8m);
        Assert.Equal(102, seleccionado.DESCRIPCION_ID);
    }

    [Fact]
    public void TipoOchoAusenteNoSeSustituyePorSeis()
    {
        Assert.Throws<InvalidOperationException>(() =>
            IvaDocumentoRetencion.ResolverTipo(new[] { Tipo(101, "I.V.A. (6%)") }, 1, 8m));
    }

    [Fact]
    public void TiposOchoDuplicadosDevuelvenError()
    {
        Assert.Throws<InvalidOperationException>(() => IvaDocumentoRetencion.ResolverTipo(new[]
        {
            Tipo(101, "I.V.A. (8%)"), Tipo(102, "I.V.A. (8%)")
        }, 1, 8m));
    }

    private static ADM_DESCRIPTIVAS Tipo(int id, string descripcion) => new()
    {
        DESCRIPCION_ID = id, CODIGO = "IVA", TITULO_ID = 1, DESCRIPCION = descripcion
    };

    [Theory]
    [InlineData("8.5")]
    [InlineData("8,5")]
    public void PorcentajeDecimalNoDependeDeLaCultura(string valor)
    {
        Assert.Equal(8.5m, IvaDocumentoRetencion.LeerPorcentaje(valor));
    }

    [Theory]
    [InlineData("-1")]
    [InlineData("101")]
    [InlineData("")]
    [InlineData("8%")]
    public void RechazaPorcentajesInvalidos(string valor)
    {
        Assert.Throws<InvalidOperationException>(() => IvaDocumentoRetencion.LeerPorcentaje(valor));
    }

    [Fact]
    public void SincronizarConservaIdentidadYNumeroAlCambiarImpuesto()
    {
        var fecha = new DateTime(2026, 10, 19);
        var existente = new ADM_RETENCIONES_OP
        {
            CODIGO_RETENCION_OP = 10, NUMERO_COMPROBANTE = "202610000001",
            FECHA_INS = fecha, TIPO_RETENCION_ID = 1
        };
        var grupo = Assert.Single(IvaDocumentoRetencion.Agrupar(new[] { Documento(672, 8m, 100m, 100m, 8m) }));
        IvaDocumentoRetencion.Actualizar(existente, grupo, "202610000099");
        IvaDocumentoRetencion.Actualizar(existente, grupo, "202610000099");
        Assert.Equal(10, existente.CODIGO_RETENCION_OP);
        Assert.Equal("202610000001", existente.NUMERO_COMPROBANTE);
        Assert.Equal(fecha, existente.FECHA_INS);
        Assert.Equal("I.V.A. (8%)", existente.EXTRA3);
        Assert.Equal(grupo.TIPO_RETENCION_ID, existente.TIPO_RETENCION_ID);
        Assert.Equal(8m, existente.MONTO_RETENCION);
    }

    [Fact]
    public void FacturaOchoPorCientoRetieneTodoElIvaYConservaSuAlicuota()
    {
        var calculo = CalculoImpuestoRetencion.Calcular(234240m, 0m, 100m, 8m);
        Assert.Equal(216888.89m, calculo.BaseImponible);
        Assert.Equal(17351.11m, calculo.MontoImpuesto);
        Assert.Equal(17351.11m, calculo.MontoRetenido);
        var grupo = Assert.Single(IvaDocumentoRetencion.Agrupar(new[]
        {
            Documento(672, 8m, 100m, calculo.BaseImponible, calculo.MontoRetenido)
        }));
        Assert.Equal("I.V.A. (8%)", grupo.EXTRA3);
        Assert.Equal("672", grupo.EXTRA1);
        Assert.Equal(1672, grupo.TIPO_RETENCION_ID);
        Assert.Equal(100m, grupo.POR_RETENCION);
        Assert.Equal(17351.11m, grupo.MONTO_RETENCION);
    }

    [Fact]
    public void SeparaAlicuotasYPorcentajesYSumaSoloDocumentosCompatibles()
    {
        var grupos = IvaDocumentoRetencion.Agrupar(new[]
        {
            Documento(672, 8m, 100m, 100m, 8m),
            Documento(672, 8m, 100m, 200m, 16m),
            Documento(673, 6m, 100m, 100m, 6m),
            Documento(672, 8m, 75m, 100m, 6m)
        });
        Assert.Equal(3, grupos.Count);
        var ocho = Assert.Single(grupos.Where(x => x.EXTRA2 == "8" && x.POR_RETENCION == 100m));
        Assert.Equal(300m, ocho.BASE_IMPONIBLE);
        Assert.Equal(24m, ocho.MONTO_RETENCION);
        Assert.Equal(500m, grupos.Sum(x => x.BASE_IMPONIBLE));
        Assert.Equal(36m, grupos.Sum(x => x.MONTO_RETENCION));
    }

    [Fact]
    public void RetencionCeroConservaImpuestoYNoGeneraLinea()
    {
        var calculo = CalculoImpuestoRetencion.Calcular(108m, 0m, 0m, 8m);
        Assert.Equal(100m, calculo.BaseImponible);
        Assert.Equal(8m, calculo.MontoImpuesto);
        Assert.Equal(0m, calculo.MontoRetenido);
        Assert.Empty(IvaDocumentoRetencion.Agrupar(new[] { Documento(672, 8m, 0m, 100m, 0m) }));
    }

    [Fact]
    public void ExentoNoSeIncluyeEnBaseGravada()
    {
        var calculo = CalculoImpuestoRetencion.Calcular(158m, 50m, 75m, 8m);
        Assert.Equal(100m, calculo.BaseImponible);
        Assert.Equal(8m, calculo.MontoImpuesto);
        Assert.Equal(6m, calculo.MontoRetenido);
    }

    [Fact]
    public void CambioDeAlicuotaNoCoincideConGrupoAnterior()
    {
        var anterior = Assert.Single(IvaDocumentoRetencion.Agrupar(new[] { Documento(672, 8m, 100m, 100m, 8m) }));
        var nuevo = Assert.Single(IvaDocumentoRetencion.Agrupar(new[] { Documento(673, 6m, 100m, 100m, 6m) }));
        Assert.True(IvaDocumentoRetencion.MismoGrupo(anterior, anterior));
        Assert.False(IvaDocumentoRetencion.MismoGrupo(anterior, nuevo));
    }

    private static IvaDocumentoRetencion.Documento Documento(int impuesto, decimal alicuota,
        decimal porcentaje, decimal baseImponible, decimal retenido) => new(new ADM_DOCUMENTOS_OP
        {
            CODIGO_ORDEN_PAGO = 18409, TIPO_IMPUESTO_ID = impuesto,
            BASE_IMPONIBLE = baseImponible, MONTO_RETENIDO = retenido,
            MONTO_IMPUESTO_EXENTO = 50m
        }, 1, porcentaje, alicuota, impuesto + 1000);
}
