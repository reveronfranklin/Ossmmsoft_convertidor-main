using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteCompromisoPresupuestario;
using Convertidor.Services.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Presupuesto.Reports.ReporteCompromisoPresupuestario
{
    public class ReporteCompromisoPresupuestarioService
    {
        private readonly IPreCompromisosService _preCompromisosService;
        private readonly IPreDetalleCompromisosService _preDetalleCompromisosService;
        private readonly IPrePucCompromisosService _prePucCompromisosService;
        private readonly IAdmSolicitudesRepository _admSolicitudesRepository;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;
        private readonly IAdmComunicacionProveedorRepository _admComProveedorRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _pRE_INDICE_CAT_PRGRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IConfiguration _configuration;
        private readonly IAdmDireccionProveedorRepository _admDirProveedorRepository;

        public ReporteCompromisoPresupuestarioService(IPreCompromisosService preCompromisosService,
                                                      IPreDetalleCompromisosService preDetalleCompromisosService,
                                                      IPrePucCompromisosService prePucCompromisosService,
                                                      IAdmSolicitudesRepository admSolicitudesRepository,
                                                      IAdmProveedoresRepository admProveedoresRepository,
                                                      IAdmDireccionProveedorRepository admDirProveedorRepository,
                                                      IAdmComunicacionProveedorRepository admComProveedorRepository,
                                                      IPRE_INDICE_CAT_PRGRepository pRE_INDICE_CAT_PRGRepository,
                                                      IAdmDescriptivaRepository admDescriptivaRepository,
                                                      IConfiguration configuration)
        {
            _preCompromisosService = preCompromisosService;
            _preDetalleCompromisosService = preDetalleCompromisosService;
            _prePucCompromisosService = prePucCompromisosService;
            _admSolicitudesRepository = admSolicitudesRepository;
            _admProveedoresRepository = admProveedoresRepository;
            _admComProveedorRepository = admComProveedorRepository;
            _pRE_INDICE_CAT_PRGRepository = pRE_INDICE_CAT_PRGRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _configuration = configuration;
            _admDirProveedorRepository = admDirProveedorRepository;
        }

        public async Task<EncabezadoReporteDto> GenerateDataEncabezadoDto(FilterPreCompromisosDto filter)
        {

            try
            {
                EncabezadoReporteDto result = new EncabezadoReporteDto();
                var compromiso = await _preCompromisosService.GetByNumeroYFecha(filter.NumeroCompromiso,filter.fechaCompromiso);



                result.NumeroCompromiso = compromiso.Data.NumeroCompromiso;
              
                result.FechaCompromiso = compromiso.Data.FechaCompromiso;
                result.FechaCompromisoString = compromiso.Data.FechaCompromiso.ToString("u");
                FechaDto fechaCompromisoObj = FechaObj.GetFechaDto(compromiso.Data.FechaCompromiso);
                result.FechaCompromisoObj = (FechaDto)fechaCompromisoObj;
                var solicitud = await _admSolicitudesRepository.GetByCodigoSolicitud(compromiso.Data.CodigoSolicitud);
                result.CodigoSolicitud = solicitud.CODIGO_SOLICITUD;
                var icp = await _pRE_INDICE_CAT_PRGRepository.GetByCodigo(solicitud.CODIGO_SOLICITANTE);


                result.UnidadEjecutora = icp.UNIDAD_EJECUTORA;

                

                result.MontoEnLetras = solicitud.MONTO_LETRAS;
                if (solicitud.MONTO_LETRAS == null)
                {
                    solicitud.MONTO_LETRAS = "";
                    result.MontoEnLetras = solicitud.MONTO_LETRAS;

                }
                if (solicitud.FIRMANTE == null)
                {
                    solicitud.FIRMANTE = "";
                    result.Firmante = solicitud.FIRMANTE;

                }

                
                var Proveedor = await _admProveedoresRepository.GetByCodigo((int)solicitud.CODIGO_PROVEEDOR);
                result.NombreProveedor = Proveedor.NOMBRE_PROVEEDOR;
                result.Rif = Proveedor.RIF;

                return result;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return null;
            }

        }


    }
}
