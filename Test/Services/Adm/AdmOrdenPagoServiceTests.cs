using Moq;
using Xunit;
using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Services.Adm.AdmOrdenPago;
using Convertidor.Data.Entities.ADM;
using Convertidor.Dtos;
using Convertidor.Dtos.Sis;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Adm;
using Convertidor.Services.Presupuesto;
using Convertidor.Data.Interfaces.Sis;

namespace Convertidor.Tests.Services.Adm
{
    public class AdmOrdenPagoServiceTests
    {
        private readonly Mock<IAdmOrdenPagoRepository> _mockRepository;
        private readonly Mock<ISisUsuarioRepository> _mockSisUsuarioRepository;
        private readonly Mock<IAdmProveedoresRepository> _mockProveedoresRepository;
        private readonly Mock<IPRE_PRESUPUESTOSRepository> _mockPresupuestosRepository;
        private readonly Mock<IAdmDescriptivaRepository> _mockDescriptivaRepository;
        private readonly Mock<IAdmCompromisoOpService> _mockCompromisoOpService;
        private readonly Mock<IPreCompromisosService> _mockCompromisosService;
        private readonly Mock<IPreDetalleCompromisosRepository> _mockDetalleCompromisosRepository;
        private readonly Mock<IPrePucCompromisosRepository> _mockPucCompromisosRepository;
        private readonly Mock<IAdmPucOrdenPagoService> _mockPucOrdenPagoService;
        private readonly Mock<IPRE_V_SALDOSRepository> _mockSaldosRepository;
        private readonly Mock<IAdmBeneficariosOpService> _mockBeneficariosOpService;
        private readonly Mock<IAdmPucOrdenPagoRepository> _mockPucOrdenPagoRepository;
        private readonly Mock<IAdmDocumentosOpRepository> _mockDocumentosOpRepository;
        private readonly Mock<IAdmBeneficiariosOpRepository> _mockBeneficiariosOpRepository;
        private readonly Mock<IAdmCompromisosPendientesRepository> _mockCompromisosPendientesRepository;
        
        private readonly AdmOrdenPagoService _service;

        public AdmOrdenPagoServiceTests()
        {
            _mockRepository = new Mock<IAdmOrdenPagoRepository>();
            _mockSisUsuarioRepository = new Mock<ISisUsuarioRepository>();
            _mockProveedoresRepository = new Mock<IAdmProveedoresRepository>();
            _mockPresupuestosRepository = new Mock<IPRE_PRESUPUESTOSRepository>();
            _mockDescriptivaRepository = new Mock<IAdmDescriptivaRepository>();
            _mockCompromisoOpService = new Mock<IAdmCompromisoOpService>();
            _mockCompromisosService = new Mock<IPreCompromisosService>();
            _mockDetalleCompromisosRepository = new Mock<IPreDetalleCompromisosRepository>();
            _mockPucCompromisosRepository = new Mock<IPrePucCompromisosRepository>();
            _mockPucOrdenPagoService = new Mock<IAdmPucOrdenPagoService>();
            _mockSaldosRepository = new Mock<IPRE_V_SALDOSRepository>();
            _mockBeneficariosOpService = new Mock<IAdmBeneficariosOpService>();
            _mockPucOrdenPagoRepository = new Mock<IAdmPucOrdenPagoRepository>();
            _mockDocumentosOpRepository = new Mock<IAdmDocumentosOpRepository>();
            _mockBeneficiariosOpRepository = new Mock<IAdmBeneficiariosOpRepository>();
            _mockCompromisosPendientesRepository = new Mock<IAdmCompromisosPendientesRepository>();

            _service = new AdmOrdenPagoService(
                _mockRepository.Object,
                _mockSisUsuarioRepository.Object,
                _mockProveedoresRepository.Object,
                _mockPresupuestosRepository.Object,
                _mockDescriptivaRepository.Object,
                _mockCompromisoOpService.Object,
                _mockCompromisosService.Object,
                _mockDetalleCompromisosRepository.Object,
                _mockPucCompromisosRepository.Object,
                _mockPucOrdenPagoService.Object,
                _mockSaldosRepository.Object,
                _mockBeneficariosOpService.Object,
                _mockPucOrdenPagoRepository.Object,
                _mockDocumentosOpRepository.Object,
                _mockBeneficiariosOpRepository.Object,
                _mockCompromisosPendientesRepository.Object
            );
        }

        [Fact]
        public async Task Create_WithValidData_ShouldReturnSuccess()
        {
            // Arrange
            var dto = new AdmOrdenPagoUpdateDto
            {
                CodigoOrdenPago = 0,
                CodigoCompromiso = 1,
                FechaOrdenPago = DateTime.Now,
                TipoOrdenPagoId = 1,
                CantidadPago = 1000,
                FrecuenciaPagoId = 1,
                TipoPagoId = 1,
                Motivo = "Test motivo",
                CodigoPresupuesto = 1,
                NumeroComprobante = 123,
                FechaComprobante = DateTime.Now,
                ConFactura = false
            };

              var conectado = new UserConectadoDto { Empresa = 13, Usuario = 1 };
            var presupuesto = new Data.Entities.Presupuesto.PRE_PRESUPUESTOS { ANO = 2025, CODIGO_PRESUPUESTO = 19, CODIGO_EMPRESA = 13 };
            var compromiso = new PreCompromisosResponseDto 
            { 
                CodigoCompromiso = 1, 
                CodigoProveedor = 1,
                FechaCompromiso = DateTime.Now.AddDays(-1)
            };
            ADM_V_COMPROMISO_PENDIENTE compromisoPendiente = new ADM_V_COMPROMISO_PENDIENTE();
            var ordenPagoCreada = new ADM_ORDEN_PAGO { CODIGO_ORDEN_PAGO = 1, CODIGO_PRESUPUESTO = 19 };
            ResultDto<ADM_ORDEN_PAGO> resultOrdenPagoCreada = new ResultDto<ADM_ORDEN_PAGO>(null);
            resultOrdenPagoCreada.Data = ordenPagoCreada;
            resultOrdenPagoCreada.IsValid = true;
            resultOrdenPagoCreada.Message = "";

            _mockSisUsuarioRepository.Setup(x => x.GetConectado()).ReturnsAsync(conectado);
            _mockRepository.Setup(x => x.GetCodigoOrdenPago(0)).ReturnsAsync((ADM_ORDEN_PAGO)null);
            _mockCompromisosService.Setup(x => x.GetByCompromiso(dto.CodigoCompromiso)).ReturnsAsync(compromiso);
            _mockDescriptivaRepository.Setup(x => x.GetByCodigo(dto.TipoOrdenPagoId)).ReturnsAsync(new ADM_DESCRIPTIVAS());
            _mockDescriptivaRepository.Setup(x => x.GetByCodigo(dto.FrecuenciaPagoId)).ReturnsAsync(new ADM_DESCRIPTIVAS());
            _mockDescriptivaRepository.Setup(x => x.GetByCodigo(dto.TipoPagoId)).ReturnsAsync(new ADM_DESCRIPTIVAS());
            _mockPresupuestosRepository.Setup(x => x.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto)).ReturnsAsync(presupuesto);
            _mockCompromisosPendientesRepository.Setup(x => x.GetCompromisosPendientesPorCodigoCompromiso(compromiso.CodigoCompromiso))
                .ReturnsAsync(compromisoPendiente);
            _mockRepository.Setup(x => x.GetNextKey()).ReturnsAsync(1);
            _mockRepository.Setup(x => x.GetNextOrdenPago(presupuesto.CODIGO_PRESUPUESTO)).ReturnsAsync("");


            _mockRepository.Setup(x => x.Add(It.IsAny<ADM_ORDEN_PAGO>()))
                .ReturnsAsync(resultOrdenPagoCreada);
            _mockProveedoresRepository.Setup(x => x.GetByCodigo(It.IsAny<int>())).ReturnsAsync(new ADM_PROVEEDORES());
            _mockDescriptivaRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<ADM_DESCRIPTIVAS>());

            // Act
            var result = await _service.Create(dto);

            // Assert
            Assert.True(result.IsValid);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.CodigoOrdenPago);
        }

        [Fact]
        public async Task Create_WithExistingCodigoOrdenPago_ShouldReturnError()
        {
            // Arrange
            var dto = new AdmOrdenPagoUpdateDto { CodigoOrdenPago = 1 };
            var existingOrdenPago = new ADM_ORDEN_PAGO();

            _mockRepository.Setup(x => x.GetCodigoOrdenPago((int)dto.CodigoOrdenPago)).ReturnsAsync(existingOrdenPago);

            // Act
            var result = await _service.Create(dto);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("Codigo  existe", result.Message);
        }

        [Fact]
        public async Task Update_WithValidData_ShouldReturnSuccess()
        {
            // Arrange
            var dto = new AdmOrdenPagoUpdateDto
            {
                CodigoOrdenPago = 1,
                CodigoCompromiso = 1,
                FechaOrdenPago = DateTime.Now,
                TipoOrdenPagoId = 1,
                CantidadPago = 1000,
                FrecuenciaPagoId = 1,
                TipoPagoId = 1,
                Motivo = "Test motivo actualizado",
                CodigoPresupuesto = 1,
                NumeroComprobante = 123,
                FechaComprobante = DateTime.Now,
                ConFactura = false
            };

               var conectado = new UserConectadoDto { Empresa = 13, Usuario = 1 };
            var existingOrdenPago = new ADM_ORDEN_PAGO { CODIGO_ORDEN_PAGO = 1, STATUS = "PE" };
            var compromiso = new PreCompromisosResponseDto 
            { 
                CodigoCompromiso = 1, 
                CodigoProveedor = 1,
                FechaCompromiso = DateTime.Now.AddDays(-1)
            };
            var presupuesto = new Data.Entities.Presupuesto.PRE_PRESUPUESTOS { ANO = 2025, CODIGO_PRESUPUESTO = 19, CODIGO_EMPRESA = 13 };

            _mockSisUsuarioRepository.Setup(x => x.GetConectado()).ReturnsAsync(conectado);
            _mockRepository.Setup(x => x.GetCodigoOrdenPago((int)dto.CodigoOrdenPago)).ReturnsAsync(existingOrdenPago);
            _mockRepository.Setup(x => x.Update(It.IsAny<ADM_ORDEN_PAGO>())).ReturnsAsync( new ResultDto<ADM_ORDEN_PAGO> (null));
            _mockCompromisosService.Setup(x => x.GetByCompromiso(dto.CodigoCompromiso)).ReturnsAsync(compromiso);
            _mockDescriptivaRepository.Setup(x => x.GetByCodigo(dto.TipoOrdenPagoId)).ReturnsAsync(new ADM_DESCRIPTIVAS());
            _mockDescriptivaRepository.Setup(x => x.GetByIdAndTitulo(15, dto.FrecuenciaPagoId)).ReturnsAsync(true);
            _mockDescriptivaRepository.Setup(x => x.GetByIdAndTitulo(16, dto.TipoPagoId)).ReturnsAsync(true);
            _mockPresupuestosRepository.Setup(x => x.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto)).ReturnsAsync(presupuesto);
            _mockProveedoresRepository.Setup(x => x.GetByCodigo(It.IsAny<int>())).ReturnsAsync(new ADM_PROVEEDORES());
            _mockDescriptivaRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<ADM_DESCRIPTIVAS>());

            // Act
            var result = await _service.Update(dto);

            // Assert
            Assert.True(result.IsValid);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task Delete_WithValidOrdenPago_ShouldReturnSuccess()
        {
            // Arrange
            var dto = new AdmOrdenPagoDeleteDto { CodigoOrdenPago = 1 };
            var existingOrdenPago = new ADM_ORDEN_PAGO { CODIGO_ORDEN_PAGO = 1, STATUS = "PE", CODIGO_PRESUPUESTO = 1 };

            _mockRepository.Setup(x => x.GetCodigoOrdenPago(dto.CodigoOrdenPago)).ReturnsAsync(existingOrdenPago);
            _mockRepository.Setup(x => x.Delete(dto.CodigoOrdenPago)).ReturnsAsync("");
            _mockPucOrdenPagoRepository.Setup(x => x.GetByOrdenPago(dto.CodigoOrdenPago))
                .ReturnsAsync(new List<ADM_PUC_ORDEN_PAGO>());

            // Act
            var result = await _service.Delete(dto);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(dto, result.Data);
        }

        [Fact]
        public async Task Aprobar_WithValidOrdenPago_ShouldReturnSuccess()
        {
            // Arrange
            var dto = new AdmOrdenPagoAprobarAnular { CodigoOrdenPago = 1 };
                var conectado = new UserConectadoDto { Empresa = 13, Usuario = 1 };
            var existingOrdenPago = new ADM_ORDEN_PAGO 
            { 
                CODIGO_ORDEN_PAGO = 1, 
                STATUS = "PE",
                CODIGO_PRESUPUESTO = 1
            };


            var existingOrdenPagoAprobado = new ADM_ORDEN_PAGO 
            { 
                CODIGO_ORDEN_PAGO = 1, 
                STATUS = "AP",
                CODIGO_PRESUPUESTO = 19
            };
            ResultDto<ADM_ORDEN_PAGO> resultDto = new ResultDto<ADM_ORDEN_PAGO> (null);  
            resultDto.Data = existingOrdenPagoAprobado;
            resultDto.IsValid = true;
            resultDto.Message = "";

            _mockSisUsuarioRepository.Setup(x => x.GetConectado()).ReturnsAsync(conectado);
            _mockRepository.Setup(x => x.GetCodigoOrdenPago(dto.CodigoOrdenPago)).ReturnsAsync(existingOrdenPago);
             _mockRepository.Setup(x => x.Update(It.IsAny<ADM_ORDEN_PAGO>())).ReturnsAsync(resultDto);
            _mockProveedoresRepository.Setup(x => x.GetByCodigo(It.IsAny<int>())).ReturnsAsync(new ADM_PROVEEDORES());
            _mockDescriptivaRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<ADM_DESCRIPTIVAS>());

            // Act
            var result = await _service.Aprobar(dto);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal("AP", result.Data.Status);
        }

        [Fact]
        public async Task Anular_WithValidOrdenPago_ShouldReturnSuccess()
        {
            // Arrange
            var dto = new AdmOrdenPagoAprobarAnular { CodigoOrdenPago = 1 };
            var conectado = new UserConectadoDto { Empresa = 13, Usuario = 1 };
            var existingOrdenPago = new ADM_ORDEN_PAGO 
            { 
                CODIGO_ORDEN_PAGO = 1, 
                STATUS = "AP",
                CODIGO_PRESUPUESTO = 19
            };

            

            _mockSisUsuarioRepository.Setup(x => x.GetConectado()).ReturnsAsync(conectado);
            _mockRepository.Setup(x => x.GetCodigoOrdenPago(dto.CodigoOrdenPago)).ReturnsAsync(existingOrdenPago);
            var existingOrdenPagoAnulado = new ADM_ORDEN_PAGO 
            { 
                CODIGO_ORDEN_PAGO = 1, 
                STATUS = "AN",
                CODIGO_PRESUPUESTO = 19
            };

            var listPuc=new List<ADM_PUC_ORDEN_PAGO>();
            var pucOrdenPago=new ADM_PUC_ORDEN_PAGO();
            pucOrdenPago.MONTO_PAGADO=100;
            pucOrdenPago.CODIGO_ORDEN_PAGO=1;
            pucOrdenPago.CODIGO_PUC_ORDEN_PAGO=1;
            listPuc.Add(pucOrdenPago);
          

            _mockPucOrdenPagoRepository.Setup(x => x.GetByOrdenPago(dto.CodigoOrdenPago)).ReturnsAsync(listPuc);
         
            ResultDto<ADM_ORDEN_PAGO> resultDto = new ResultDto<ADM_ORDEN_PAGO> (null);  
            resultDto.Data = existingOrdenPagoAnulado;
            resultDto.IsValid = true;
            resultDto.Message = "";

                _mockRepository.Setup(x => x.Update(It.IsAny<ADM_ORDEN_PAGO>()))
                .ReturnsAsync(resultDto);
            _mockBeneficiariosOpRepository.Setup(x => x.UpdateMontoAnulado(dto.CodigoOrdenPago)).Returns(Task.FromResult(""));
       
            _mockProveedoresRepository.Setup(x => x.GetByCodigo(It.IsAny<int>())).ReturnsAsync(new ADM_PROVEEDORES());
            _mockDescriptivaRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<ADM_DESCRIPTIVAS>());

            // Act
            var result = await _service.Anular(dto);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal("AN", result.Data.Status);
        }

        [Fact]
        public async Task GetByPresupuesto_WithValidFilter_ShouldReturnData()
        {
            // Arrange
            var filter = new AdmOrdenPagoFilterDto { CodigoPresupuesto = 1, PageNumber = 1, PageSize = 10 };
            var ordenPagos = new List<ADM_ORDEN_PAGO>
            {
                new ADM_ORDEN_PAGO { CODIGO_ORDEN_PAGO = 1, CODIGO_PROVEEDOR = 1 }
            };

            ResultDto<List<ADM_ORDEN_PAGO>> resultRepository =new   ResultDto<List<ADM_ORDEN_PAGO>> (null);
             resultRepository.Data = ordenPagos;
                resultRepository.IsValid = true;
                resultRepository.CantidadRegistros = 1;
                resultRepository.TotalPage = 1;
                resultRepository.Page = 1;
          

            _mockRepository.Setup(x => x.GetByPresupuesto(filter)).ReturnsAsync(resultRepository);
            _mockProveedoresRepository.Setup(x => x.GetByCodigo(It.IsAny<int>())).ReturnsAsync(new ADM_PROVEEDORES());
            _mockDescriptivaRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<ADM_DESCRIPTIVAS>());

            // Act
            var result = await _service.GetByPresupuesto(filter);

            // Assert
            Assert.True(result.IsValid);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
        }

        [Fact]
        public async Task OrdenDePagoEsModificable_WithPendingStatus_ShouldReturnTrue()
        {
            // Arrange
            var codigoOrdenPago = 1;
            var ordenPago = new ADM_ORDEN_PAGO { CODIGO_ORDEN_PAGO = 1, STATUS = "PE" };

            _mockRepository.Setup(x => x.GetCodigoOrdenPago(codigoOrdenPago)).ReturnsAsync(ordenPago);
            _mockPucOrdenPagoRepository.Setup(x => x.GetByOrdenPago(codigoOrdenPago))
                .ReturnsAsync(new List<ADM_PUC_ORDEN_PAGO>());

            // Act
            var result = await _service.OrdenDePagoEsModificable(codigoOrdenPago);

            // Assert
            Assert.True(result);
        }

     
 
    }
}