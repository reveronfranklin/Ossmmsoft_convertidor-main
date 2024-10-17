using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.ReporteSolicitudCompromiso;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Presupuesto.Reports.ReporteSolicitudModificacionPresupuestaria;
using Convertidor.Utility;
using iText.Layout.Renderer;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Asn1.Cmp;
using QuestPDF.Fluent;
using System.Collections.Generic;

namespace Convertidor.Services.Adm.ReporteSolicitudCompromiso
{
    public class ReporteSolicitudCompromisoService : IReporteSolicitudCompromisoService
    {
        private readonly IAdmSolicitudesService _admSolicitudesService;
        private readonly IAdmSolicitudesRepository _admSolicitudesRepository;
        private readonly IAdmDetalleSolicitudService _admDetalleSolicitudService;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;
        private readonly IAdmDireccionProveedorRepository _admDirProveedorRepository;
        private readonly IAdmComunicacionProveedorRepository _admComProveedorRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _pRE_INDICE_CAT_PRGRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IOssConfigRepository _ossConfigRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPersonasRepository _rhPersonasRepository;
        private readonly IAdmDetalleSolicitudRepository _admDetalleSolicitudRepository;

        public ReporteSolicitudCompromisoService(IAdmSolicitudesService admSolicitudesService,
                                                 IAdmSolicitudesRepository admSolicitudesRepository,
                                                 IAdmDetalleSolicitudService admDetalleSolicitudService,
                                                 IAdmProveedoresRepository admProveedoresRepository,
                                                 IAdmDireccionProveedorRepository admDirProveedorRepository,
                                                 IAdmComunicacionProveedorRepository admComProveedorRepository,
                                                 IPRE_INDICE_CAT_PRGRepository pRE_INDICE_CAT_PRGRepository,
                                                 IAdmDescriptivaRepository admDescriptivaRepository,
                                                 IConfiguration configuration,
                                                 IWebHostEnvironment env,
                                                 IOssConfigRepository ossConfigRepository,
                                                 ISisUsuarioRepository sisUsuarioRepository,
                                                 IRhPersonasRepository rhPersonasRepository,
                                                 IAdmDetalleSolicitudRepository admDetalleSolicitudRepository)
        {
            _admSolicitudesService = admSolicitudesService;
            _admSolicitudesRepository = admSolicitudesRepository;
            _admDetalleSolicitudService = admDetalleSolicitudService;
            _admProveedoresRepository = admProveedoresRepository;
            _admDirProveedorRepository = admDirProveedorRepository;
            _admComProveedorRepository = admComProveedorRepository;
            _pRE_INDICE_CAT_PRGRepository = pRE_INDICE_CAT_PRGRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _configuration = configuration;
            _env = env;
            _ossConfigRepository = ossConfigRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhPersonasRepository = rhPersonasRepository;
            _admDetalleSolicitudRepository = admDetalleSolicitudRepository;
        }

        public async Task<ReporteSolicitudCompromisoDto> GenerateData (AdmSolicitudesFilterDto filter) 
        {
          ReporteSolicitudCompromisoDto result = new ReporteSolicitudCompromisoDto();

          await _admSolicitudesRepository.LimpiaCaractereDetalle(filter.CodigoSolicitud);
          var encabezado = await GenerateDataEncabezadoDto (filter);
          var cuerpo = await GenerateDataCuerpoDto (filter);
        
          result.Encabezado = encabezado;
          result.Cuerpo = cuerpo;

          return result;
        
        }

        public async Task<EncabezadoReporteDto> GenerateDataEncabezadoDto(AdmSolicitudesFilterDto filter)
        {
         
            try
            {
                EncabezadoReporteDto result = new EncabezadoReporteDto();
                
               
                
                //var solicitud = await _admSolicitudesRepository.GetByCodigoSolicitud(filter.CodigoSolicitud);
                
                
                //Buscamos los totales y agregamos a el emcabezado
                var totales =
                    await _admDetalleSolicitudService.GetTotales((int)filter.CodigoPresupuesto,
                        filter.CodigoSolicitud);
                
                await _admSolicitudesRepository.UpdateMontoEnLetras(filter.CodigoSolicitud,totales.TotalMasImpuesto);

                await _admDetalleSolicitudRepository.LimpiaEnrer();
                var solicitud = await _admSolicitudesRepository.GetByCodigoSolicitud(filter.CodigoSolicitud);

                
                result.Base = totales.Base;
                result.Impuesto = totales.Impuesto;
                result.TotalMasImpuesto = totales.TotalMasImpuesto;
                result.PorcentajeImpuesto = totales.PorcentajeImpuesto;
                
                
                
                result.CodigoSolicitud = solicitud.CODIGO_SOLICITUD;
                if(solicitud.ANO != null) 
                {
                    result.Ano = (int)solicitud.ANO;
                }

                result.NumeroSolicitud = solicitud.NUMERO_SOLICITUD;
                result.FechaSolicitud = solicitud.FECHA_SOLICITUD;
                result.FechaSolicitudString = solicitud.FECHA_SOLICITUD.ToString("u");
                FechaDto FechaSolicitudObj = FechaObj.GetFechaDto(solicitud.FECHA_SOLICITUD);
                result.FechaSolicitudObj = (FechaDto)FechaSolicitudObj;
                result.CodigoSolicitante = solicitud.CODIGO_SOLICITANTE;

                var icp = await _pRE_INDICE_CAT_PRGRepository.GetByCodigo(solicitud.CODIGO_SOLICITANTE);
                if (icp != null)
                {
                    result.Para = "";
                    var configPara = _ossConfigRepository.GetByClave("PARA_SOLICITUD_Y_COMPROMISO");
                    if (configPara.Result != null)
                    {

                        var unidadPara = int.Parse(configPara.Result.VALOR);
                        var icpPara = await _pRE_INDICE_CAT_PRGRepository.GetByCodigo(unidadPara);
                        if (icpPara != null)
                        {
                            result.Para =icpPara.UNIDAD_EJECUTORA;
                        }
                      
                    }
                    

                    result.UnidadEjecutora = icp.UNIDAD_EJECUTORA;
                
                    var presupuesto = await _pRE_INDICE_CAT_PRGRepository.GetAllByCodigoPresupuesto(filter.CodigoPresupuesto);

                    foreach (var item in presupuesto.Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto && x.DENOMINACION.Contains("PRESUPUESTO")))
                    {
                        result.Denominacion = item.DENOMINACION;

                    }


                    if (solicitud.TIPO_SOLICITUD_ID == 825 || solicitud.TIPO_SOLICITUD_ID == 854)
                    {
                        result.RevisadoPor = "GERENCIA DE NÓMINA";
                        result.ConfirmadoPor = "DIRECCIÓN DE TALENTO HUMANO";
                    }else
                    {
                        result.RevisadoPor = "";
                        result.ConfirmadoPor ="DIRECCIÓN DE ADMINISTRACION Y FINANZAS";
                    }
                 
                }
                result.MontoLetras = solicitud.MONTO_LETRAS;
                if(solicitud.MONTO_LETRAS == null) 
                {
                    solicitud.MONTO_LETRAS = "";
                    result.MontoLetras = solicitud.MONTO_LETRAS;
                
                }

                var firmante = "";
                var usuario =await  _sisUsuarioRepository.GetByCodigo((int)solicitud.USUARIO_INS);
                if (usuario != null)
                {
                    
                    firmante =
                        $"{usuario.USUARIO} \n C.I: {usuario.CEDULA} \n FIRMA: ________________________________________________________";
                    
                 
                }
                result.Firmante = firmante;
                result.Motivo = LimpiarCaracteres.LimpiarEnter(solicitud.MOTIVO);;
                result.CodigoProveedor = (int)solicitud.CODIGO_PROVEEDOR;

                result.NombreProveedor = "";
                result.Rif = "";

                var proveedor = await _admProveedoresRepository.GetByCodigo((int)solicitud.CODIGO_PROVEEDOR);
                if(proveedor != null)
                {
                    result.NombreProveedor = proveedor.NOMBRE_PROVEEDOR;
                    
                    if (solicitud.TIPO_SOLICITUD_ID == 825 || solicitud.TIPO_SOLICITUD_ID == 854)
                    {
                        result.Rif = ""; 
                    }else
                    {
                        result.Rif = proveedor.RIF; 
                    }
                    
                  
                }


                result.Vialidad = "";
                result.Vivienda = "";

                var dirProveedor = await _admDirProveedorRepository.GetByCodigoProveedor(proveedor.CODIGO_PROVEEDOR);
                if(dirProveedor != null) 
                {
                    if (dirProveedor.VIALIDAD == null && dirProveedor.VIVIENDA == null)
                    {
                        dirProveedor.VIALIDAD = " ";
                        dirProveedor.VIVIENDA = " ";
                    }
                    if (dirProveedor.PRINCIPAL == 1)
                    {
                        result.Vialidad = dirProveedor.VIALIDAD;
                        result.Vivienda = dirProveedor.VIVIENDA;


                        

                        var extra1 = await _admDescriptivaRepository.GetByCodigoDescriptiva(dirProveedor.TIPO_VIVIENDA_ID);
                        if (extra1 != null)
                        {
                            result.Extra1 = extra1.EXTRA1;
                        }
                        else
                        {
                            result.Extra1 = " ";
                        }

                        
                    }

                    result.CodigoArea = "";
                    result.LineaComunicacion = "";

                    var comProveedor = await _admComProveedorRepository.GetBycodigoProveedor(proveedor.CODIGO_PROVEEDOR);
                    if (comProveedor != null)
                    {
                        result.CodigoArea = comProveedor.CODIGO_AREA;
                        result.LineaComunicacion = comProveedor.LINEA_COMUNICACION;
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

        public async Task<List<CuerpoReporteDto>> GenerateDataCuerpoDto(AdmSolicitudesFilterDto filter)
        {
            try
            {
                List<CuerpoReporteDto> result = new List<CuerpoReporteDto>();
                var detalle =await   _admDetalleSolicitudService.GetByCodigoSolicitud(filter);
                var tipoImpuesto = 0;
                string variableImpuesto = "DESCRIPTIVA_IMPUESTO";
                var config = await _ossConfigRepository.GetByClave(variableImpuesto);
                if (config != null)
                {
                    tipoImpuesto = int.Parse(config.VALOR);
                }
                CuerpoReporteDto itemIva = new CuerpoReporteDto();
                var detalleImpuesto = detalle.Data.Where(x => x.TipoImpuestoId == tipoImpuesto).FirstOrDefault();
                

                if (detalle.Data.Count > 0 )
                     {
                  
                            foreach (var item in detalle.Data)
                            {

                                CuerpoReporteDto resultItem = new CuerpoReporteDto();

                                resultItem.Cantidad = item.Cantidad;

                                var descriptiva = await _admDescriptivaRepository.GetByCodigo(item.UdmId);

                                resultItem.DescripcionUdmId = descriptiva.DESCRIPCION;
                                resultItem.DescripcionArticulo = LimpiarCaracteres.LimpiarEnter( item.Descripcion);
                                resultItem.PrecioUnitario = item.PrecioUnitario;
                                resultItem.TotalBolivares = (decimal)item.Total;


                                if (tipoImpuesto != item.TipoImpuestoId)
                                {
                                    result.Add(resultItem);
                                }
                               

                            }

                            if (detalleImpuesto != null)
                            {
                                CuerpoReporteDto resultItem = new CuerpoReporteDto();

                                resultItem.Cantidad = detalleImpuesto.Cantidad;

                                var descriptiva = await _admDescriptivaRepository.GetByCodigo(detalleImpuesto.UdmId);

                                resultItem.DescripcionUdmId = descriptiva.DESCRIPCION;
                                resultItem.DescripcionArticulo = LimpiarCaracteres.LimpiarEnter( detalleImpuesto.Descripcion);
                                resultItem.PrecioUnitario = detalleImpuesto.PrecioUnitario;
                                resultItem.TotalBolivares = (decimal)detalleImpuesto.Total;
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
        public async Task<string> ReportData(AdmSolicitudesFilterDto filter)
        {
           

            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var result = "No Data";
            var pathLogo = @settings.BmFiles + "LogoIzquierda.jpeg";
            var fileName = $"ReporteSolicitudCompromiso-{filter.CodigoSolicitud}.pdf";
            var separatorPatch = @settings.SeparatorPatch;
            var filePath = $"{@settings.ExcelFiles}/{fileName}.pdf";


            if (filter == null)
            {
                return null;
            }

            if (!File.Exists(pathLogo))
            {
                return $"No existe {pathLogo}";
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
                    var document = new ReporteSolicitudCompromisoDocument(reporte, pathLogo);
                    document.GeneratePdf(filePath);
                }

                return result;
            }

            return result;
        }

    }
}
