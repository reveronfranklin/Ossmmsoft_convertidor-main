using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteCompromisoPresupuestario;
using Convertidor.Services.Presupuesto.ReporteCompromisoPresupuestario;
using Convertidor.Services.Presupuesto.ReporteOrdenSercicioPresupuestario;
using Convertidor.Utility;
using QuestPDF.Fluent;

namespace Convertidor.Services.Presupuesto.Reports.ReporteOrdenSercicioPresupuestario
{
    public class ReporteOrdenServicioPresupuestarioService : IReporteOrdenServicioPresupuestarioService
    {
        private readonly IPreCompromisosRepository _preCompromisosRepository;
        private readonly IPreDetalleCompromisosService _preDetalleCompromisosService;
        private readonly IPrePucCompromisosService _prePucCompromisosService;
        private readonly IAdmSolicitudesRepository _admSolicitudesRepository;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;
        private readonly IPRE_V_SALDOSRepository _preV_SALDOSRepository;
        private readonly IPrePucCompromisosRepository _prePucCompromisosRepository;
        private readonly IAdmComunicacionProveedorRepository _admComProveedorRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _pRE_INDICE_CAT_PRGRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IConfiguration _configuration;
        private readonly IPreDetalleCompromisosRepository _preDetalleCompromisosRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IOssConfigRepository _ossConfigRepository;
        private readonly IAdmDireccionProveedorRepository _admDireccionProveedorRepository;


        public ReporteOrdenServicioPresupuestarioService(IPreCompromisosRepository preCompromisosRepository,
                                                      IPreDetalleCompromisosService preDetalleCompromisosService,
                                                      IPrePucCompromisosService prePucCompromisosService,
                                                      IAdmSolicitudesRepository admSolicitudesRepository,
                                                      IAdmProveedoresRepository admProveedoresRepository,
                                                      IPRE_V_SALDOSRepository preV_SALDOSRepository,
                                                      IPrePucCompromisosRepository prePucCompromisosRepository,
                                                      IAdmComunicacionProveedorRepository admComProveedorRepository,
                                                      IPRE_INDICE_CAT_PRGRepository pRE_INDICE_CAT_PRGRepository,
                                                      IAdmDescriptivaRepository admDescriptivaRepository,
                                                      IConfiguration configuration,
                                                      IPreDetalleCompromisosRepository preDetalleCompromisosRepository,
                                                      ISisUsuarioRepository sisUsuarioRepository,
                                                      IOssConfigRepository ossConfigRepository,
                                                      IAdmDireccionProveedorRepository admDireccionProveedorRepository)
        {
            _preCompromisosRepository = preCompromisosRepository;
            _preDetalleCompromisosService = preDetalleCompromisosService;
            _prePucCompromisosService = prePucCompromisosService;
            _admSolicitudesRepository = admSolicitudesRepository;
            _admProveedoresRepository = admProveedoresRepository;
            _preV_SALDOSRepository = preV_SALDOSRepository;
            _prePucCompromisosRepository = prePucCompromisosRepository;
            _admComProveedorRepository = admComProveedorRepository;
            _pRE_INDICE_CAT_PRGRepository = pRE_INDICE_CAT_PRGRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _configuration = configuration;
            _preDetalleCompromisosRepository = preDetalleCompromisosRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _ossConfigRepository = ossConfigRepository;
            _admDireccionProveedorRepository = admDireccionProveedorRepository;
        }

        public async Task<ReporteCompromisoPresupuestarioDto> GenerateData(FilterReporteBySolicitud filter)
        {
            ReporteCompromisoPresupuestarioDto result = new ReporteCompromisoPresupuestarioDto();
            var compromiso = await _preCompromisosRepository.GetByCodigoSolicitud(filter.CodigoSolModificacion);

            await _preDetalleCompromisosRepository.LimpiaEnrer();
            var encabezado = await GenerateDataEncabezadoDto(filter);
            encabezado.PucCompromisos =await  GetPuc(compromiso.CODIGO_COMPROMISO);
            var cuerpo = await GenerateDataCuerpoDto(filter);

                result.Encabezado = encabezado;
            result.Cuerpo = cuerpo;

            return result;

        }

        public async Task<List<PrePucCompromisosResponseDto>> GetPuc(int codigoCompromiso)
        {
            List<PrePucCompromisosResponseDto> result = new List<PrePucCompromisosResponseDto>();
            var detalle = await _preDetalleCompromisosService.GetByCodigoCompromiso(codigoCompromiso);
            if (detalle.Count > 0)
            {

                foreach (var item in detalle)
                {

                  
                        
                    var pucCompromisos = await _prePucCompromisosService.GetByDetalleCompromiso(item.CodigoDetalleCompromiso);
                    if(pucCompromisos.Data.Count > 0) 
                    {
                        result.AddRange(pucCompromisos.Data);
                          
                    }
                

                }

                    
                return result;
            }
            return result;
        }

        public async Task<string> GetFirmante(int codigoSolicitud)
        {

            string result = "";
            var solicitud = await _admSolicitudesRepository.GetByCodigoSolicitud(codigoSolicitud);
            if (solicitud != null)
            {
                var usuario =await  _sisUsuarioRepository.GetByCodigo((int)solicitud.USUARIO_INS);
                if (usuario != null)
                {
                    
                    result =
                        $"{usuario.USUARIO} \n C.I: {usuario.CEDULA}";
                    
                 
                }
            }
            else
            {
                result = "";
            }
            return result;

        }
        
        public async Task<EncabezadoReporteDto> GenerateDataEncabezadoDto(FilterReporteBySolicitud filter)
        {

            try
            {
                EncabezadoReporteDto result = new EncabezadoReporteDto();
                var compromiso = await _preCompromisosRepository.GetByCodigoSolicitud(filter.CodigoSolModificacion);
             
                var totales = await _preDetalleCompromisosRepository.GetTotales(compromiso.CODIGO_PRESUPUESTO,compromiso.CODIGO_COMPROMISO);

                
                decimal totalMasImpuesto =totales.TotalMasImpuesto;
                decimal totalImpuesto = totales.Impuesto;
                decimal total = totales.Base;
                
                _preCompromisosRepository.UpdateMontoEnLetras(compromiso.CODIGO_COMPROMISO, totalMasImpuesto);
                compromiso = await _preCompromisosRepository.GetByCodigoSolicitud(filter.CodigoSolModificacion);

                result.NumeroCompromiso = compromiso.NUMERO_COMPROMISO;
                result.FechaCompromiso = compromiso.FECHA_COMPROMISO;
                result.FechaCompromisoString = compromiso.FECHA_COMPROMISO.ToString("u");
                FechaDto fechaCompromisoObj = FechaObj.GetFechaDto(compromiso.FECHA_COMPROMISO);
                result.FechaCompromisoObj = (FechaDto)fechaCompromisoObj;
                var solicitud = await _admSolicitudesRepository.GetByCodigoSolicitud(compromiso.CODIGO_SOLICITUD);
                result.NumeroSolicitud = solicitud.NUMERO_SOLICITUD;
                var icp = await _pRE_INDICE_CAT_PRGRepository.GetByCodigo(solicitud.CODIGO_SOLICITANTE);
                result.Denominacion = icp.DENOMINACION;
                var icpConcat = $"{icp.CODIGO_SECTOR}.{icp.CODIGO_PROGRAMA}.{icp.CODIGO_SUBPROGRAMA}.{icp.CODIGO_PROYECTO}.{icp.CODIGO_ACTIVIDAD}";
                result.IcpConcat = icpConcat;
                result.codigoSector = icp.CODIGO_SECTOR;
                result.codigoPrograma = icp.CODIGO_PROGRAMA;
                result.codigoSubPrograma = icp.CODIGO_SUBPROGRAMA;
                result.codigoProyecto = icp.CODIGO_PROYECTO;
                result.codigoActividad = icp.CODIGO_ACTIVIDAD;
                result.TolalMasImpuesto = totalMasImpuesto;
                result.Impuesto = totalImpuesto;
                result.Tolal = total;
                result.Base = total;
                result.MontoEnLetras = compromiso.MONTO_LETRAS;
                result.PorcentajeImpuesto = totales.PorcentajeImpuesto;
                result.Titulo = "";
                var ossConfig = await _ossConfigRepository.GetByClave(solicitud.TIPO_SOLICITUD_ID.ToString());
                if (ossConfig != null)
                {
                    result.Titulo =ossConfig.VALOR;
                }
              
                
                result.Firmante = await GetFirmante(filter.CodigoSolModificacion);
              

                
                var Proveedor = await _admProveedoresRepository.GetByCodigo((int)solicitud.CODIGO_PROVEEDOR);
                result.NombreProveedor = Proveedor.NOMBRE_PROVEEDOR;
                result.Rif = Proveedor.RIF;
                result.Direccion = "";
                var dirProveedor =
                    await _admDireccionProveedorRepository.GetByCodigoProveedor((int)solicitud.CODIGO_PROVEEDOR);
                if (dirProveedor != null)
                {
                    result.Direccion = $"{dirProveedor.VIALIDAD} {dirProveedor.VIVIENDA}";
                }
                var comProveedor = await _admComProveedorRepository.GetBycodigoProveedor(Proveedor.CODIGO_PROVEEDOR);
                if (comProveedor != null) 
                {
                    result.CodigoArea = comProveedor.CODIGO_AREA;
                    result.LineaComunicacion = comProveedor.LINEA_COMUNICACION;
                }
              
                result.Motivo = LimpiarCaracteres.LimpiarEnter(compromiso.MOTIVO);;
                result.Para = "";
                var configPara =await _ossConfigRepository.GetByClave("PARA_SOLICITUD_Y_COMPROMISO");
                if (configPara != null)
                {

                    var unidadPara = int.Parse(configPara.VALOR);
                    var icpPara = await _pRE_INDICE_CAT_PRGRepository.GetByCodigo(unidadPara);
                    if (icpPara != null)
                    {
                        result.Para =icpPara.UNIDAD_EJECUTORA;
                    }
                      
                }


                return result;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return null;
            }

        }


        public async Task<List<CuerpoReporteDto>> GenerateDataCuerpoDto(FilterReporteBySolicitud filter)
        {
            try
            {
                List<CuerpoReporteDto> result = new List<CuerpoReporteDto>();
                var compromiso = await _preCompromisosRepository.GetByCodigoSolicitud(filter.CodigoSolModificacion);

                var detalle = await _preDetalleCompromisosService.GetByCodigoCompromiso(compromiso.CODIGO_COMPROMISO);

                if(detalle == null) 
                {
                    return null;
                }

                if (detalle.Count > 0)
                {

                    foreach (var item in detalle)
                    {

                        CuerpoReporteDto resultItem = new CuerpoReporteDto();

                        resultItem.Cantidad = item.Cantidad;

                        var descriptiva = await _admDescriptivaRepository.GetByCodigo(item.UdmId);

                        resultItem.CodigoDetalleCompromiso = item.CodigoDetalleCompromiso;
                        resultItem.DescripcionUdm = descriptiva.DESCRIPCION;
                        resultItem.DescripcionArticulo = item.Descripcion;
                        
                        //resultItem.DescripcionArticulo = LimpiarCaracteres.LimpiarEnter(item.Descripcion);
                        resultItem.PrecioUnitario = item.PrecioUnitario;
                        resultItem.TotalBolivares = (item.PrecioUnitario * item.Cantidad);
                        
                        var pucCompromisos = await _prePucCompromisosService.GetByDetalleCompromiso(item.CodigoDetalleCompromiso);
                        if(pucCompromisos.Data.Count > 0) 
                        {
                          resultItem.PucCompromisos = pucCompromisos.Data;
                          
                        }
                        result.Add(resultItem);

                    }

                    
                    return result;
                }

                return result;



            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return null;
            }


        }

        public async Task<string> ReportData(FilterReporteBySolicitud filter)
        {

            if (filter == null)
            {
                return "No Data";
            }
            var solicitud = await _admSolicitudesRepository.GetByCodigoSolicitud(filter.CodigoSolModificacion);
            if (solicitud == null)
            {
                return "No Data";
            }
            var compromiso = await _preCompromisosRepository.GetByCodigoSolicitud(filter.CodigoSolModificacion);
            if (compromiso == null)
            {
                return "No Data";
            }
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var result = "No Data";
            var pathLogo = @settings.BmFiles + "LogoIzquierda.jpeg";
            var fileName = $"ReporteOrdenServicioPresupuestario-{filter.CodigoSolModificacion}-{compromiso.NUMERO_COMPROMISO}.pdf";
            var separatorPatch = @settings.SeparatorPatch;
            var filePath = $"{@settings.ExcelFiles}{separatorPatch}{fileName}.pdf";


          
            var ossConfig = await _ossConfigRepository.GetByClave(solicitud.TIPO_SOLICITUD_ID.ToString());
            if (ossConfig == null)
            {
                return "No Data";
            }


            var reporte = await GenerateData(filter);
            if (reporte != null)
            {






                if (reporte == null)
                {
                    return "No Data";
                }
                else
                {


                    filePath = $"{@settings.ExcelFiles}{separatorPatch}{fileName}";


                    result = fileName;
                    var document = new ReporteOrdenServicioPresupuestarioDocument(reporte, pathLogo);
                    document.GeneratePdf(filePath);
                }

                return result;
            }

            return result;
        }


    }
}
