using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Sis;
using Convertidor.Services.Presupuesto;
using Moq;
using Xunit;

namespace Convertidor.Tests.Services.Presupuesto;

public class CompromisosDisponiblesTests
{
    [Theory]
    [InlineData("", 1)]
    [InlineData("CMP", 1)]
    [InlineData("CMP-PE", 0)]
    public async Task Lista_SoloIncluyeAprobados_AntesDePaginar(string texto, int esperados)
    {
        var compromisos = new Mock<IPreCompromisosRepository>();
        var presupuestos = new Mock<IPRE_PRESUPUESTOSRepository>();
        var usuario = new Mock<ISisUsuarioRepository>();
        var pendientes = new Mock<IAdmCompromisosPendientesRepository>();
        var candidatos = new List<ADM_V_COMPROMISO_PENDIENTE>();
        var estados = new[] { "PE", "AP", "AN" };
        for (var i = 0; i < estados.Length; i++)
        {
            var codigo = i + 1;
            candidatos.Add(new ADM_V_COMPROMISO_PENDIENTE {
                CODIGO_IDENTIFICADOR = codigo, NUMERO_IDENTIFICADOR = "CMP-" + estados[i],
                CODIGO_PRESUPUESTO = 20, MONTO_POR_CAUSAR = 100, MOTIVO = "Prueba"
            });
            compromisos.Setup(x => x.GetByCodigo(codigo)).ReturnsAsync(new PRE_COMPROMISOS {
                CODIGO_COMPROMISO = codigo, STATUS = estados[i], FECHA_COMPROMISO = new DateTime(2026, 9, 20)
            });
        }
        usuario.Setup(x => x.GetConectado()).ReturnsAsync(new UserConectadoDto { Empresa = 13 });
        presupuestos.Setup(x => x.GetByCodigo(13, 20)).ReturnsAsync(new PRE_PRESUPUESTOS { ANO = 2026 });
        pendientes.Setup(x => x.GetCompromisosPendientesPorCodigoPresupuesto(20)).ReturnsAsync(candidatos);
        var service = new PreCompromisosService(
            compromisos.Object, presupuestos.Object, null!, null!, null!, null!, null!, null!,
            usuario.Object, null!, null!, null!, null!, null!,
            Mock.Of<IPreDetalleCompromisosRepository>(), null!, null!, pendientes.Object,
            Mock.Of<IAdmSolicitudesRepository>());

        var result = await service.GetCompromisosPendientesByPresupuesto(new PreCompromisosFilterDto {
            CodigoPresupuesto = 20, PageNumber = 0, PageSize = 1, SearchText = texto
        });

        Assert.True(result.IsValid, result.Message);
        Assert.Equal(esperados, result.CantidadRegistros);
        Assert.Equal(esperados, result.Data.Count);
        Assert.All(result.Data, item => Assert.Equal("AP", item.Status));
        if (esperados > 0) Assert.Equal("CMP-AP", result.Data[0].NumeroCompromiso);
    }
}
