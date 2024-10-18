using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteCompromisoPresupuestario;
using Convertidor.Services.Presupuesto.ReporteCompromisoPresupuestario;
using Convertidor.Utility;
using QuestPDF.Fluent;

namespace Convertidor.Services.Presupuesto.Reports.ReporteCompromisoPresupuestario
{
    public class ReporteCompromisoPresupuestarioService : IReporteCompromisoPresupuestarioService
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


        public ReporteCompromisoPresupuestarioService(IPreCompromisosRepository preCompromisosRepository,
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
                                                      IOssConfigRepository ossConfigRepository)
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
        }

        public async Task<ReporteCompromisoPresupuestarioDto> GenerateData(FilterPreCompromisosDto filter)
        {
            ReporteCompromisoPresupuestarioDto result = new ReporteCompromisoPresupuestarioDto();
            await _preDetalleCompromisosRepository.LimpiaEnrer();
            var encabezado = await GenerateDataEncabezadoDto(filter);
            encabezado.PucCompromisos =await  GetPuc(filter.CodigoCompromiso);
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

        public async Task<string> GetFirmante(int codigoCompromiso)
        {

            string result = "";


            var detalle = await _preDetalleCompromisosRepository.GetByCodigoCompromiso(codigoCompromiso);
            if (detalle.Any())
            {
                var ultimoDetalle = detalle.OrderByDescending(x => x.CODIGO_DETALLE_COMPROMISO).FirstOrDefault();
                var listPuc =
                    await _prePucCompromisosRepository.GetListByCodigoDetalleCompromiso(ultimoDetalle
                        .CODIGO_DETALLE_COMPROMISO);
                if (listPuc != null)
                {
                    var ultimoPuc = listPuc.OrderByDescending(x => x.CODIGO_PUC_COMPROMISO).FirstOrDefault();
                    if (ultimoPuc != null)
                    {
                      
                        var usuario =await  _sisUsuarioRepository.GetByCodigo((int)ultimoPuc.USUARIO_INS);
                        if (usuario != null)
                        {
                    
                            result =
                                $"{usuario.USUARIO} \n C.I: {usuario.CEDULA}";
                    
                 
                        }
                    }
                }
            }
            else
            {
                result = "";
            }
            return result;

        }
        
        public async Task<EncabezadoReporteDto> GenerateDataEncabezadoDto(FilterPreCompromisosDto filter)
        {

            try
            {
                EncabezadoReporteDto result = new EncabezadoReporteDto();
                var compromiso = await _preCompromisosRepository.GetByNumeroYFecha(filter.NumeroCompromiso,filter.fechaCompromiso);
             
                var totales = await _preDetalleCompromisosRepository.GetTotales(compromiso.CODIGO_PRESUPUESTO,filter.CodigoCompromiso);

                
                decimal totalMasImpuesto =totales.TotalMasImpuesto;
                decimal totalImpuesto = totales.Impuesto;
                decimal total = totales.Base;
                
                _preCompromisosRepository.UpdateMontoEnLetras(filter.CodigoCompromiso, totalMasImpuesto);
                compromiso = await _preCompromisosRepository.GetByNumeroYFecha(filter.NumeroCompromiso,filter.fechaCompromiso);
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
               
               
                
                
                result.Firmante = await GetFirmante(compromiso.CODIGO_COMPROMISO);
              

                
                var Proveedor = await _admProveedoresRepository.GetByCodigo((int)solicitud.CODIGO_PROVEEDOR);
                result.NombreProveedor = Proveedor.NOMBRE_PROVEEDOR;
                result.Rif = Proveedor.RIF;

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


        public async Task<List<CuerpoReporteDto>> GenerateDataCuerpoDto(FilterPreCompromisosDto filter)
        {
            try
            {
                List<CuerpoReporteDto> result = new List<CuerpoReporteDto>();
             
                var detalle = await _preDetalleCompromisosService.GetByCodigoCompromiso(filter.CodigoCompromiso);

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

        public async Task<string> ReportData(FilterPreCompromisosDto filter)
        {


            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var result = "No Data";
            var pathLogo = @settings.BmFiles + "LogoIzquierda.jpeg";
            var fileName = $"ReporteCompromisoPresupuestario-{filter.CodigoCompromiso}.pdf";
            var separatorPatch = @settings.SeparatorPatch;
            var filePath = $"{@settings.ExcelFiles}{separatorPatch}{fileName}.pdf";


            if (filter == null)
            {
                return null;
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
                    var document = new ReporteCompromisoPresupuestarioDocument(reporte, pathLogo);
                    document.GeneratePdf(filePath);
                }

                return result;
            }

            return result;
        }


    }
}
