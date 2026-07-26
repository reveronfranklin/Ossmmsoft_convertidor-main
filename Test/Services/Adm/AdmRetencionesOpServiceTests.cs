using Moq;
using Xunit;
using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Sis;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Adm;
using Convertidor.Services.Adm;
using Convertidor.Services.Adm.AdmRetencionesOp;
using Convertidor.Services.Presupuesto;
using Microsoft.Extensions.Logging.Abstractions;

namespace Convertidor.Tests.Services.Adm
{
    public class AdmRetencionesOpServiceTests
    {
        private readonly Mock<IAdmRetencionesOpRepository> _mockRepository;
        private readonly Mock<ISisUsuarioRepository> _mockSisUsuarioRepository;
        private readonly Mock<IPRE_PRESUPUESTOSRepository> _mockPresupuestosRepository;
        private readonly Mock<IAdmOrdenPagoRepository> _mockOrdenPagoRepository;
        private readonly Mock<IAdmDescriptivaRepository> _mockDescriptivaRepository;
        private readonly Mock<IAdmRetencionesRepository> _mockRetencionesRepository;
        private readonly Mock<ISisSerieDocumentosRepository> _mockSerieDocumentosRepository;
        private readonly Mock<ISisDescriptivaRepository> _mockSisDescriptivaRepository;
        private readonly Mock<IAdmDocumentosOpRepository> _mockDocumentosOpRepository;
        private readonly Mock<IOssConfigRepository> _mockOssConfigRepository;
        private readonly Mock<IAdmBeneficariosOpService> _mockBeneficariosOpService;
        private readonly Mock<IAdmCompromisoOpRepository> _mockCompromisoOpRepository;
        private readonly Mock<IPreDetalleCompromisosRepository> _mockDetalleCompromisosRepository;
        private readonly Mock<IAdmPucOrdenPagoRepository> _mockPucOrdenPagoRepository;

        private readonly AdmRetencionesOpService _service;

        public AdmRetencionesOpServiceTests()
        {
            _mockRepository = new Mock<IAdmRetencionesOpRepository>();
            _mockSisUsuarioRepository = new Mock<ISisUsuarioRepository>();
            _mockPresupuestosRepository = new Mock<IPRE_PRESUPUESTOSRepository>();
            _mockOrdenPagoRepository = new Mock<IAdmOrdenPagoRepository>();
            _mockDescriptivaRepository = new Mock<IAdmDescriptivaRepository>();
            _mockRetencionesRepository = new Mock<IAdmRetencionesRepository>();
            _mockSerieDocumentosRepository = new Mock<ISisSerieDocumentosRepository>();
            _mockSisDescriptivaRepository = new Mock<ISisDescriptivaRepository>();
            _mockDocumentosOpRepository = new Mock<IAdmDocumentosOpRepository>();
            _mockOssConfigRepository = new Mock<IOssConfigRepository>();
            _mockBeneficariosOpService = new Mock<IAdmBeneficariosOpService>();
            _mockCompromisoOpRepository = new Mock<IAdmCompromisoOpRepository>();
            _mockDetalleCompromisosRepository = new Mock<IPreDetalleCompromisosRepository>();
            _mockPucOrdenPagoRepository = new Mock<IAdmPucOrdenPagoRepository>();

            _service = new AdmRetencionesOpService(
                _mockRepository.Object,
                _mockSisUsuarioRepository.Object,
                _mockPresupuestosRepository.Object,
                _mockOrdenPagoRepository.Object,
                _mockDescriptivaRepository.Object,
                _mockRetencionesRepository.Object,
                _mockSerieDocumentosRepository.Object,
                _mockSisDescriptivaRepository.Object,
                _mockDocumentosOpRepository.Object,
                _mockOssConfigRepository.Object,
                _mockBeneficariosOpService.Object,
                _mockCompromisoOpRepository.Object,
                _mockDetalleCompromisosRepository.Object,
                _mockPucOrdenPagoRepository.Object,
                NullLogger<AdmRetencionesOpService>.Instance
            );

            _mockDocumentosOpRepository.Setup(x => x.GetByCodigoOrdenPago(It.IsAny<int>()))
                .ReturnsAsync(new List<ADM_DOCUMENTOS_OP>());
        }

        private static ADM_DESCRIPTIVAS DescriptivaIva(int id = 10) => new ADM_DESCRIPTIVAS { DESCRIPCION_ID = id, CODIGO = "IVA" };

        private static SIS_DESCRIPTIVAS SisDescriptivaIva(int descripcionId = 20) => new SIS_DESCRIPTIVAS { DESCRIPCION_ID = descripcionId, EXTRA1 = "IVA" };

        [Fact]
        public async Task AsignarComprobanteIvaOrdenPago_SinRetencionesIva_ShouldReturnNoAplica()
        {
            _mockRepository.Setup(x => x.GetByOrdenPago(1)).ReturnsAsync(new List<ADM_RETENCIONES_OP>());

            var result = await _service.AsignarComprobanteIvaOrdenPago(1);

            Assert.True(result.IsValid);
            Assert.Equal(EstadoAsignacionComprobante.NoAplica, result.Data.Estado);
            _mockSerieDocumentosRepository.Verify(x => x.ReservarSerieAtomica(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task AsignarComprobanteIvaOrdenPago_ConNumeroExistenteEnOrden_ShouldReutilizar()
        {
            var retencion = new ADM_RETENCIONES_OP { CODIGO_RETENCION_OP = 1, CODIGO_ORDEN_PAGO = 1, TIPO_RETENCION_ID = 5, NUMERO_COMPROBANTE = "" };
            _mockRepository.Setup(x => x.GetByOrdenPago(1)).ReturnsAsync(new List<ADM_RETENCIONES_OP> { retencion });
            _mockDescriptivaRepository.Setup(x => x.GetByCodigo(5)).ReturnsAsync(DescriptivaIva());
            _mockOrdenPagoRepository.Setup(x => x.GetCodigoOrdenPago(1))
                .ReturnsAsync(new ADM_ORDEN_PAGO { CODIGO_ORDEN_PAGO = 1, NUMERO_COMPROBANTE = 999 });
            _mockRepository.Setup(x => x.UpdaNumeroComprobante(1, "999"))
                .ReturnsAsync(new ResultDto<int>(1) { IsValid = true });

            var result = await _service.AsignarComprobanteIvaOrdenPago(1);

            Assert.True(result.IsValid);
            Assert.Equal(EstadoAsignacionComprobante.Reutilizado, result.Data.Estado);
            Assert.Equal(999, result.Data.NumeroComprobante);
            _mockSerieDocumentosRepository.Verify(x => x.ReservarSerieAtomica(It.IsAny<int>()), Times.Never);
            _mockRepository.Verify(x => x.UpdaNumeroComprobante(1, "999"), Times.Once);
        }

        [Fact]
        public async Task AsignarComprobanteIvaOrdenPago_ConNumeroExistenteEnRetencion_ShouldReutilizarYBackfillOrden()
        {
            var retencion = new ADM_RETENCIONES_OP { CODIGO_RETENCION_OP = 1, CODIGO_ORDEN_PAGO = 1, TIPO_RETENCION_ID = 5, NUMERO_COMPROBANTE = "777" };
            _mockRepository.Setup(x => x.GetByOrdenPago(1)).ReturnsAsync(new List<ADM_RETENCIONES_OP> { retencion });
            _mockDescriptivaRepository.Setup(x => x.GetByCodigo(5)).ReturnsAsync(DescriptivaIva());
            _mockOrdenPagoRepository.Setup(x => x.GetCodigoOrdenPago(1))
                .ReturnsAsync(new ADM_ORDEN_PAGO { CODIGO_ORDEN_PAGO = 1, NUMERO_COMPROBANTE = null });
            _mockOrdenPagoRepository.Setup(x => x.UpdateNumeroComprobante(1, 777))
                .ReturnsAsync(new ResultDto<int>(1) { IsValid = true });

            var result = await _service.AsignarComprobanteIvaOrdenPago(1);

            Assert.True(result.IsValid);
            Assert.Equal(EstadoAsignacionComprobante.Reutilizado, result.Data.Estado);
            Assert.Equal(777, result.Data.NumeroComprobante);
            _mockOrdenPagoRepository.Verify(x => x.UpdateNumeroComprobante(1, 777), Times.Once);
            _mockSerieDocumentosRepository.Verify(x => x.ReservarSerieAtomica(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task AsignarComprobanteIvaOrdenPago_RetencionSinDescriptivaConfigurada_ShouldReturnError()
        {
            var retencion = new ADM_RETENCIONES_OP { CODIGO_RETENCION_OP = 1, CODIGO_ORDEN_PAGO = 1, TIPO_RETENCION_ID = 5 };
            _mockRepository.Setup(x => x.GetByOrdenPago(1)).ReturnsAsync(new List<ADM_RETENCIONES_OP> { retencion });
            _mockDescriptivaRepository.Setup(x => x.GetByCodigo(5)).ReturnsAsync((ADM_DESCRIPTIVAS)null);

            var result = await _service.AsignarComprobanteIvaOrdenPago(1);

            Assert.False(result.IsValid);
            Assert.Equal(EstadoAsignacionComprobante.Error, result.Data.Estado);
        }

        [Fact]
        public async Task AsignarComprobanteIvaOrdenPago_SinDescriptivaSisIva_ShouldReturnError()
        {
            var retencion = new ADM_RETENCIONES_OP { CODIGO_RETENCION_OP = 1, CODIGO_ORDEN_PAGO = 1, TIPO_RETENCION_ID = 5 };
            _mockRepository.Setup(x => x.GetByOrdenPago(1)).ReturnsAsync(new List<ADM_RETENCIONES_OP> { retencion });
            _mockDescriptivaRepository.Setup(x => x.GetByCodigo(5)).ReturnsAsync(DescriptivaIva());
            _mockOrdenPagoRepository.Setup(x => x.GetCodigoOrdenPago(1))
                .ReturnsAsync(new ADM_ORDEN_PAGO { CODIGO_ORDEN_PAGO = 1 });
            _mockSisDescriptivaRepository.Setup(x => x.GetALL()).ReturnsAsync(new List<SIS_DESCRIPTIVAS>());

            var result = await _service.AsignarComprobanteIvaOrdenPago(1);

            Assert.False(result.IsValid);
            Assert.Equal(EstadoAsignacionComprobante.Error, result.Data.Estado);
            Assert.Contains("descriptiva", result.Message);
        }

        [Fact]
        public async Task AsignarComprobanteIvaOrdenPago_ConDescriptivaSisAmbigua_ShouldReturnError()
        {
            var retencion = new ADM_RETENCIONES_OP { CODIGO_RETENCION_OP = 1, CODIGO_ORDEN_PAGO = 1, TIPO_RETENCION_ID = 5 };
            _mockRepository.Setup(x => x.GetByOrdenPago(1)).ReturnsAsync(new List<ADM_RETENCIONES_OP> { retencion });
            _mockDescriptivaRepository.Setup(x => x.GetByCodigo(5)).ReturnsAsync(DescriptivaIva());
            _mockOrdenPagoRepository.Setup(x => x.GetCodigoOrdenPago(1))
                .ReturnsAsync(new ADM_ORDEN_PAGO { CODIGO_ORDEN_PAGO = 1 });
            _mockSisDescriptivaRepository.Setup(x => x.GetALL())
                .ReturnsAsync(new List<SIS_DESCRIPTIVAS> { SisDescriptivaIva(20), SisDescriptivaIva(21) });

            var result = await _service.AsignarComprobanteIvaOrdenPago(1);

            Assert.False(result.IsValid);
            Assert.Equal(EstadoAsignacionComprobante.Error, result.Data.Estado);
            Assert.Contains("ambigua", result.Message);
            _mockSerieDocumentosRepository.Verify(x => x.ReservarSerieAtomica(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task AsignarComprobanteIvaOrdenPago_FalloReservaSerie_ShouldReturnError()
        {
            var retencion = new ADM_RETENCIONES_OP { CODIGO_RETENCION_OP = 1, CODIGO_ORDEN_PAGO = 1, TIPO_RETENCION_ID = 5 };
            _mockRepository.Setup(x => x.GetByOrdenPago(1)).ReturnsAsync(new List<ADM_RETENCIONES_OP> { retencion });
            _mockDescriptivaRepository.Setup(x => x.GetByCodigo(5)).ReturnsAsync(DescriptivaIva());
            _mockOrdenPagoRepository.Setup(x => x.GetCodigoOrdenPago(1))
                .ReturnsAsync(new ADM_ORDEN_PAGO { CODIGO_ORDEN_PAGO = 1 });
            _mockSisDescriptivaRepository.Setup(x => x.GetALL())
                .ReturnsAsync(new List<SIS_DESCRIPTIVAS> { SisDescriptivaIva(20) });
            _mockSerieDocumentosRepository.Setup(x => x.ReservarSerieAtomica(20))
                .ReturnsAsync(new ResultDto<string>("")
                {
                    IsValid = false,
                    Message = "No existe serie de documentos activa para el tipo de documento configurado"
                });

            var result = await _service.AsignarComprobanteIvaOrdenPago(1);

            Assert.False(result.IsValid);
            Assert.Equal(EstadoAsignacionComprobante.Error, result.Data.Estado);
            Assert.Equal("No existe serie de documentos activa para el tipo de documento configurado", result.Message);
        }

        [Fact]
        public async Task AsignarComprobanteIvaOrdenPago_SerieNoNumerica_ShouldReturnError()
        {
            var retencion = new ADM_RETENCIONES_OP { CODIGO_RETENCION_OP = 1, CODIGO_ORDEN_PAGO = 1, TIPO_RETENCION_ID = 5 };
            _mockRepository.Setup(x => x.GetByOrdenPago(1)).ReturnsAsync(new List<ADM_RETENCIONES_OP> { retencion });
            _mockDescriptivaRepository.Setup(x => x.GetByCodigo(5)).ReturnsAsync(DescriptivaIva());
            _mockOrdenPagoRepository.Setup(x => x.GetCodigoOrdenPago(1))
                .ReturnsAsync(new ADM_ORDEN_PAGO { CODIGO_ORDEN_PAGO = 1 });
            _mockSisDescriptivaRepository.Setup(x => x.GetALL())
                .ReturnsAsync(new List<SIS_DESCRIPTIVAS> { SisDescriptivaIva(20) });
            _mockSerieDocumentosRepository.Setup(x => x.ReservarSerieAtomica(20))
                .ReturnsAsync(new ResultDto<string>("ABC-001") { IsValid = true, Message = "" });

            var result = await _service.AsignarComprobanteIvaOrdenPago(1);

            Assert.False(result.IsValid);
            Assert.Equal(EstadoAsignacionComprobante.Error, result.Data.Estado);
        }

        [Fact]
        public async Task AsignarComprobanteIvaOrdenPago_FalloAlPersistirEnRetencion_ShouldReturnError()
        {
            var retencion = new ADM_RETENCIONES_OP { CODIGO_RETENCION_OP = 1, CODIGO_ORDEN_PAGO = 1, TIPO_RETENCION_ID = 5 };
            _mockRepository.Setup(x => x.GetByOrdenPago(1)).ReturnsAsync(new List<ADM_RETENCIONES_OP> { retencion });
            _mockDescriptivaRepository.Setup(x => x.GetByCodigo(5)).ReturnsAsync(DescriptivaIva());
            _mockOrdenPagoRepository.Setup(x => x.GetCodigoOrdenPago(1))
                .ReturnsAsync(new ADM_ORDEN_PAGO { CODIGO_ORDEN_PAGO = 1 });
            _mockSisDescriptivaRepository.Setup(x => x.GetALL())
                .ReturnsAsync(new List<SIS_DESCRIPTIVAS> { SisDescriptivaIva(20) });
            _mockSerieDocumentosRepository.Setup(x => x.ReservarSerieAtomica(20))
                .ReturnsAsync(new ResultDto<string>("1001") { IsValid = true, Message = "" });
            _mockOrdenPagoRepository.Setup(x => x.UpdateNumeroComprobante(1, 1001))
                .ReturnsAsync(new ResultDto<int>(1) { IsValid = true });
            _mockRepository.Setup(x => x.UpdaNumeroComprobante(1, "1001"))
                .ReturnsAsync(new ResultDto<int>(0) { IsValid = false, Message = "ORA-00001: fallo simulado" });

            var result = await _service.AsignarComprobanteIvaOrdenPago(1);

            Assert.False(result.IsValid);
            Assert.Equal(EstadoAsignacionComprobante.Error, result.Data.Estado);
            Assert.Contains("1001", result.Message);
        }

        [Fact]
        public async Task AsignarComprobanteIvaOrdenPago_Generado_ShouldPersistirEnOrdenYRetencion()
        {
            var retencion = new ADM_RETENCIONES_OP { CODIGO_RETENCION_OP = 1, CODIGO_ORDEN_PAGO = 1, TIPO_RETENCION_ID = 5 };
            _mockRepository.Setup(x => x.GetByOrdenPago(1)).ReturnsAsync(new List<ADM_RETENCIONES_OP> { retencion });
            _mockDescriptivaRepository.Setup(x => x.GetByCodigo(5)).ReturnsAsync(DescriptivaIva());
            _mockOrdenPagoRepository.Setup(x => x.GetCodigoOrdenPago(1))
                .ReturnsAsync(new ADM_ORDEN_PAGO { CODIGO_ORDEN_PAGO = 1 });
            _mockSisDescriptivaRepository.Setup(x => x.GetALL())
                .ReturnsAsync(new List<SIS_DESCRIPTIVAS> { SisDescriptivaIva(20) });
            _mockSerieDocumentosRepository.Setup(x => x.ReservarSerieAtomica(20))
                .ReturnsAsync(new ResultDto<string>("1001") { IsValid = true, Message = "" });
            _mockOrdenPagoRepository.Setup(x => x.UpdateNumeroComprobante(1, 1001))
                .ReturnsAsync(new ResultDto<int>(1) { IsValid = true });
            _mockRepository.Setup(x => x.UpdaNumeroComprobante(1, "1001"))
                .ReturnsAsync(new ResultDto<int>(1) { IsValid = true });

            var result = await _service.AsignarComprobanteIvaOrdenPago(1);

            Assert.True(result.IsValid);
            Assert.Equal(EstadoAsignacionComprobante.Generado, result.Data.Estado);
            Assert.Equal(1001, result.Data.NumeroComprobante);
            Assert.Equal(1, result.Data.CantidadRetencionesActualizadas);
            _mockOrdenPagoRepository.Verify(x => x.UpdateNumeroComprobante(1, 1001), Times.Once);
            _mockRepository.Verify(x => x.UpdaNumeroComprobante(1, "1001"), Times.Once);
        }
    }
}
