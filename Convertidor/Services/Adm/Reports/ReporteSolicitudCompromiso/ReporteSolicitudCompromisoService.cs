using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.ReporteSolicitudCompromiso;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Convertidor.Services.Adm.Reports.ReporteSolicitudCompromiso;
public class ReporteSolicitudCompromisoService : IReporteSolicitudCompromisoService
{
    private readonly IAdmSolCompromisoService _admSolCompromisoService;
    private readonly IAdmDetalleSolCompromisoService _admDetalleSolCompromisoService;
    private readonly IAdmPucSolCompromisoService _admPucSolCompromisoService;
    private readonly IPRE_PRESUPUESTOSRepository _preSUPUESTOSRepository;
    private readonly IAdmProveedoresService _admProveedoresService;
    private readonly IConfiguration _configuration;

    public ReporteSolicitudCompromisoService(IAdmSolCompromisoService admSolCompromisoService,
                                             IAdmDetalleSolCompromisoService admDetalleSolCompromisoService,
                                             IAdmPucSolCompromisoService admPucSolCompromisoService,
                                                       IPRE_PRESUPUESTOSRepository preSUPUESTOSRepository,
                                                       IAdmProveedoresService admProveedoresService,
                                                             IConfiguration configuration)
    {
        _admSolCompromisoService = admSolCompromisoService;
        _admDetalleSolCompromisoService = admDetalleSolCompromisoService;
        _admPucSolCompromisoService = admPucSolCompromisoService;
        _preSUPUESTOSRepository = preSUPUESTOSRepository;
        _admProveedoresService = admProveedoresService;
        _configuration = configuration;
    }

   

    public async Task<ReporteSolicitudCompromisoDto> GenerateData(int codigosolCompromiso,int codigoDetalleSolicitud,int codigoPucSolicitud)
    {
        ReporteSolicitudCompromisoDto result = new ReporteSolicitudCompromisoDto();
        

        var solicitud = await GenerateDataSolicitud(codigosolCompromiso);
        var detalle = await GenerateDataDetalle(codigoDetalleSolicitud);
        var pucSolCompromiso = await GenerateDataPucsolicitud(codigoPucSolicitud);
        result.SolicitudCompromiso = solicitud;
        result.DetalleSolicitud = detalle;
        result.PucSolicitudCompromiso = pucSolCompromiso;

        return result;

    }

    public FechaDto GetFechaDto(DateTime fecha)
    {
        var FechaDesdeObj = new FechaDto();
        FechaDesdeObj.Year = fecha.Year.ToString();
        string month = "00" + fecha.Month.ToString();
        string day = "00" + fecha.Day.ToString();
        FechaDesdeObj.Month = month.Substring(month.Length - 2);
        FechaDesdeObj.Day = day.Substring(day.Length - 2);

        return FechaDesdeObj;
    }

    public async Task<SolicitudcompromisoDto> GenerateDataSolicitud(int codigosolCompromiso)
    {

        SolicitudcompromisoDto result = new SolicitudcompromisoDto();
        var solicitudCompromiso = await _admSolCompromisoService.GetByCodigo(codigosolCompromiso);
        if (solicitudCompromiso != null)
        {
            result.CodigoSolCompromiso = solicitudCompromiso.CodigoSolCompromiso;
            result.TipoSolCompromisoId = solicitudCompromiso.TipoSolCompromisoId;
            result.FechaSolicitud = solicitudCompromiso.FechaSolicitud;
            result.FechaSolicitudString = solicitudCompromiso.FechaSolicitud.ToString("u");
            FechaDto fechaSolicitudObj = GetFechaDto(solicitudCompromiso.FechaSolicitud);
            result.FechaSolicitudObj = (FechaDto)fechaSolicitudObj;
            result.NumeroSolicitud = solicitudCompromiso.NumeroSolicitud;
            result.CodigoSolicitante = solicitudCompromiso.CodigoSolicitante;
            result.CodigoProveedor = solicitudCompromiso.CodigoProveedor;
            result.Motivo = solicitudCompromiso.Motivo;
            result.Status = solicitudCompromiso.Status;
            result.CodigoPresupuesto = solicitudCompromiso.CodigoPresupuesto;
            result.Ano = solicitudCompromiso.Ano;
            result.Extra1 = solicitudCompromiso.Extra1;
            result.Extra2 = solicitudCompromiso.Extra2;
            result.Extra3 = solicitudCompromiso.Extra3;
        }

        return result;


    }

    public async Task<List<DetalleSolicitudcompromisoDto>> GenerateDataDetalle(int codigoDetalleSolicitud)
    {
        List<DetalleSolicitudcompromisoDto> result = new List<DetalleSolicitudcompromisoDto>();

        var Detalle = await _admDetalleSolCompromisoService.GetAllByCodigoDetalleSolicitud(codigoDetalleSolicitud);

        if (Detalle == null)
        {
            return null;
        }

        var listDto = Detalle;


       if (listDto != null)
       {


            foreach (var item in listDto)
            {

               DetalleSolicitudcompromisoDto resultItem = new DetalleSolicitudcompromisoDto();


                resultItem.CodigoDetalleSolicitud = item.CodigoDetalleSolicitud;
                resultItem.CodigoPucSolicitud = item.CodigoPucSolicitud;
                resultItem.Cantidad = item.Cantidad;
                resultItem.UdmId = item.UdmId;
                resultItem.Denominacion = item.Denominacion;
                resultItem.PrecioUnitario = item.PrecioUnitario;
                resultItem.TipoImpuestoId = item.TipoImpuestoId;
                resultItem.PORIMPUESTO = item.PORIMPUESTO;
                resultItem.CantidadAprobada = item.CantidadAprobada;
                resultItem.CantidadAnulada = item.CantidadAnulada;
                resultItem.Extra1 = item.Extra1;
                resultItem.Extra2 = item.Extra2;
                resultItem.Extra3 = item.Extra3;
                resultItem.CodigoPresupuesto = item.CodigoPresupuesto;



                result.Add(resultItem);

            }

            return result;
        }

        return result;

    }

    public async Task<List<PucSolCompromisoDto>> GenerateDataPucsolicitud(int codigoPucSolicitud)
    {
        List<PucSolCompromisoDto> result = new List<PucSolCompromisoDto>();
       


        var solicitudCompromiso = await _admPucSolCompromisoService.GetAllbyCodigoPucSolcicitud(codigoPucSolicitud);
        if (solicitudCompromiso != null)
        {
            foreach (var item in solicitudCompromiso)
            {
                PucSolCompromisoDto resultItem = new PucSolCompromisoDto();

                resultItem.CodigoPucSolicitud = item.CodigoPucSolicitud;
                resultItem.CodigoSolicitud = item.CodigoSolicitud;
                resultItem.CodigoSaldo = item.CodigoSaldo;
                resultItem.CodigoIcp = item.CodigoIcp;
                resultItem.CodigoPuc = item.CodigoPuc;
                resultItem.FinanciadoId = item.FinanciadoId;
                resultItem.CodigoFinanciado = item.CodigoFinanciado;
                resultItem.Monto = item.Monto;
                resultItem.MontoComprometido = item.MontoComprometido;
                resultItem.MontoAnulado = item.MontoAnulado;
                resultItem.Extra1 = item.Extra1;
                resultItem.Extra2 = item.Extra2;
                resultItem.Extra3 = item.Extra3;
                resultItem.CodigoPresupuesto = item.CodigoPresupuesto;

                result.Add(resultItem);
            }
        }

        return result;


    }



    public async Task<string> ReportData(FilterSolicitudCompromisoDto filter)
    {
       

        


        var settings = _configuration.GetSection("Settings").Get<Settings>();
        var result = "No Data";
        var pathLogo = @settings.BmFiles + "LogoIzquierda.jpeg";
        var fileName = $"ReporteSolicitudCompromiso-{filter.CodigoSolCompromiso}.pdf";
        var filePath = $"{@settings.ExcelFiles}/{fileName}.pdf";


        if (filter == null)
        {
            return null;
        }



        var reporte = await GenerateData(filter.CodigoSolCompromiso,filter.CodigoDetalleSolicitud,filter.CodigoPucSolicitud);
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