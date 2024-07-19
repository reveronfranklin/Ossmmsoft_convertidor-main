using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.ReporteSolicitudCompromiso;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Presupuesto.Reports.ReporteSolicitudModificacionPresupuestaria;
using Convertidor.Utility;
using iText.Layout.Renderer;
using QuestPDF.Fluent;
using System.Collections.Generic;

namespace Convertidor.Services.Adm.ReporteSolicitudCompromiso
{
    public class ReporteSolicitudCompromisoService : IReporteSolicitudCompromisoService
    {
        private readonly IAdmSolicitudesService _admSolicitudesService;
        private readonly IAdmSolicitudesRepository _admSolicitudesRepository;
        private readonly IAdmDetalleSolicitudService _admDetalleSolicitudService;
        private readonly IPreCompromisosService _preCompromisosService;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;
        private readonly IAdmDireccionProveedorRepository _admDirProveedorRepository;
        private readonly IAdmComunicacionProveedorRepository _admComProveedorRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _pRE_INDICE_CAT_PRGRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IConfiguration _configuration;

        public ReporteSolicitudCompromisoService(IAdmSolicitudesService admSolicitudesService,
                                                 IAdmSolicitudesRepository admSolicitudesRepository,
                                                 IAdmDetalleSolicitudService admDetalleSolicitudService,
                                                 IPreCompromisosService preCompromisosService,
                                                 IAdmProveedoresRepository admProveedoresRepository,
                                                 IAdmDireccionProveedorRepository admDirProveedorRepository,
                                                 IAdmComunicacionProveedorRepository admComProveedorRepository,
                                                 IPRE_INDICE_CAT_PRGRepository pRE_INDICE_CAT_PRGRepository,
                                                 IAdmDescriptivaRepository admDescriptivaRepository,
                                                 IConfiguration configuration)
        {
            _admSolicitudesService = admSolicitudesService;
            _admSolicitudesRepository = admSolicitudesRepository;
            _admDetalleSolicitudService = admDetalleSolicitudService;
            _preCompromisosService = preCompromisosService;
            _admProveedoresRepository = admProveedoresRepository;
            _admDirProveedorRepository = admDirProveedorRepository;
            _admComProveedorRepository = admComProveedorRepository;
            _pRE_INDICE_CAT_PRGRepository = pRE_INDICE_CAT_PRGRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _configuration = configuration;
        }

        public async Task<ReporteSolicitudCompromisoDto> GenerateData (AdmSolicitudesFilterDto filter) 
        {
          ReporteSolicitudCompromisoDto result = new ReporteSolicitudCompromisoDto();
          var encabezado = await GenerateDataEncabezadoDto (filter);
          var cuerpo = await GenerateDataCuerpoDto (filter);
        
          result.Encabezado = encabezado;
          result.Cuerpo = cuerpo;

          return result;
        
        }

        public async Task<EncabezadoReporteDto> GenerateDataEncabezadoDto(AdmSolicitudesFilterDto filter)
        {
                EncabezadoReporteDto result = new EncabezadoReporteDto();
                var solicitud = await _admSolicitudesRepository.GetByCodigoSolicitud(filter.CodigoSolicitud);

            
            
                result.CodigoSolicitud = solicitud.CODIGO_SOLICITUD;
                result.Ano = (int)solicitud.ANO;
                result.NumeroSolicitud = solicitud.NUMERO_SOLICITUD;
                result.FechaSolicitud = solicitud.FECHA_SOLICITUD;
                result.FechaSolicitudString = solicitud.FECHA_SOLICITUD.ToString("u");
                FechaDto FechaSolicitudObj = FechaObj.GetFechaDto(solicitud.FECHA_SOLICITUD);
                result.FechaSolicitudObj = (FechaDto)FechaSolicitudObj;
                result.CodigoSolicitante = solicitud.CODIGO_SOLICITANTE;

                var icp = await _pRE_INDICE_CAT_PRGRepository.GetByCodigo(solicitud.CODIGO_SOLICITANTE);

                result.CodigoIcp = icp.CODIGO_ICP;
                result.UnidadEjecutora = icp.UNIDAD_EJECUTORA;
                result.Denominacion = icp.DENOMINACION;
                result.CodigoProveedor = (int)solicitud.CODIGO_PROVEEDOR;

                var Proveedor = await _admProveedoresRepository.GetByCodigo((int)solicitud.CODIGO_PROVEEDOR);
                result.NombreProveedor = Proveedor.NOMBRE_PROVEEDOR;
                result.Rif = Proveedor.RIF;

                
           
                var dirProveedor = await _admDirProveedorRepository.GetByCodigoProveedor(Proveedor.CODIGO_PROVEEDOR);
                if (dirProveedor.PRINCIPAL == 1)
                {
                    result.Vialidad = dirProveedor.VIALIDAD;
                    result.Vivienda = dirProveedor.VIVIENDA;

                    var comProveedor = await _admComProveedorRepository.GetBycodigoProveedor(Proveedor.CODIGO_PROVEEDOR);
                    result.CodigoArea = comProveedor.CODIGO_AREA;
                    result.LineaComunicacion = comProveedor.LINEA_COMUNICACION;

                    var extra1 = await _admDescriptivaRepository.GetByCodigoDescriptiva(dirProveedor.TIPO_VIVIENDA_ID);
                    result.Extra1 = extra1.EXTRA1;
                }


                return result;
           
        }

        public async Task<List<CuerpoReporteDto>> GenerateDataCuerpoDto(AdmSolicitudesFilterDto filter)
        {
            try
            {
                List<CuerpoReporteDto> result = new List<CuerpoReporteDto>();
                var solicitudByPresupuesto = await _admSolicitudesService.GetByPresupuesto(filter);
                var detalle = _admDetalleSolicitudService.GetByCodigoSolicitud(filter.CodigoSolicitud);
                var detalleDatos = filter.CodigoSolicitud;



                foreach (var itemSolicitud in solicitudByPresupuesto.Data)
                {
                    itemSolicitud.CodigoSolicitud = detalleDatos;

                    foreach (var item in detalle.Data)
                    {
                        if (item.CodigoSolicitud == itemSolicitud.CodigoSolicitud)
                        {
                            CuerpoReporteDto resultItem = new CuerpoReporteDto();
                            resultItem.Cantidad = item.Cantidad;

                            var descriptiva = await _admDescriptivaRepository.GetByCodigoDescriptiva(item.UdmId);
                            resultItem.DescripcionUdmId = descriptiva.DESCRIPCION_ID;
                            resultItem.DescripcionArticulo = item.Descripcion;
                            resultItem.PrecioUnitario = item.PrecioUnitario;
                            var cantidad = item.Cantidad;
                            var precioUnitario = item.PrecioUnitario;
                            resultItem.TotalBolivares = (cantidad * precioUnitario);
                            var subTotalBolivares = detalle.Data.Sum(x => x.PrecioTotal);
                            resultItem.SubTotal = subTotalBolivares;
                            resultItem.TotalMontoImpuesto = subTotalBolivares * item.PorImpuesto / 100;
                            resultItem.Total = resultItem.TotalBolivares + resultItem.TotalMontoImpuesto;
                            resultItem.MontoImpuesto = (decimal)item.MontoImpuesto;
                            resultItem.Motivo = itemSolicitud.Motivo;
                            resultItem.TotalEnletras = (item.Total).ToString();
                            result.Add(resultItem);

                            return result;
                        }

                        return result;
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
            var filePath = $"{@settings.ExcelFiles}/{fileName}.pdf";


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


                    filePath = $"{@settings.ExcelFiles}/{fileName}";


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
