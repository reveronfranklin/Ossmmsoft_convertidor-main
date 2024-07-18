using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.ReporteSolicitudCompromiso;
using Convertidor.Utility;
using iText.Layout.Renderer;

namespace Convertidor.Services.Adm.ReporteSolicitudCompromiso
{
    public class ReporteSolicitudCompromisoService
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

        public ReporteSolicitudCompromisoService(IAdmSolicitudesService admSolicitudesService,
                                                 IAdmSolicitudesRepository admSolicitudesRepository,
                                                 IAdmDetalleSolicitudService admDetalleSolicitudService,
                                                 IPreCompromisosService preCompromisosService,
                                                 IAdmProveedoresRepository admProveedoresRepository,
                                                 IAdmDireccionProveedorRepository admDirProveedorRepository,
                                                 IAdmComunicacionProveedorRepository admComProveedorRepository,
                                                 IPRE_INDICE_CAT_PRGRepository pRE_INDICE_CAT_PRGRepository,
                                                 IAdmDescriptivaRepository admDescriptivaRepository)
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
        }

        public async Task<EncabezadoReporteDto> GenerateDataEncabezadoDto(int codigoSolicitud)
        {
            EncabezadoReporteDto result = new EncabezadoReporteDto();
            var solicitud = await _admSolicitudesRepository.GetByCodigoSolicitud(codigoSolicitud);
            result.CodigoSolicitud = solicitud.CODIGO_SOLICITUD;
            result.Ano = solicitud.ANO;
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
            result.CodigoProveedor = solicitud.CODIGO_PROVEEDOR ?? default;

            var Proveedor = await _admProveedoresRepository.GetByCodigo(solicitud.CODIGO_PROVEEDOR ?? default);
            result.NombreProveedor = Proveedor.NOMBRE_PROVEEDOR;
            result.Rif = Proveedor.RIF;

            var dirProveedor = await _admDirProveedorRepository.GetByProveedorAndPrincipal(Proveedor.CODIGO_PROVEEDOR, 1);
            result.Vialidad = dirProveedor.VIALIDAD;
            result.Vivienda = dirProveedor.VIVIENDA;

            var comProveedor = await _admComProveedorRepository.GetByProveedorAndPrincipal(Proveedor.CODIGO_PROVEEDOR, 1);
            result.CodigoArea = comProveedor.CODIGO_AREA;
            result.LineaComunicacion = comProveedor.LINEA_COMUNICACION;

            var extra1 = await _admDescriptivaRepository.GetByCodigo(3);
            result.Extra1 = extra1.EXTRA1;



            return result;


        }

    }
}
