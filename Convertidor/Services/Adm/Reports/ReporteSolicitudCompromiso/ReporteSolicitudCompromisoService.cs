using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteSolicitudModificacion;
using Convertidor.Services.Adm.Reports.ReporteSolicitudCompromiso;
using Convertidor.Services.Rh.Report.Example;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NPOI.SS.Formula.Functions;
using NuGet.Protocol.Core.Types;
using QuestPDF.Fluent;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace Convertidor.Services.Adm.Reports.ReporteSolicitudCompromiso;
    public class ReporteSolicitudCompromisoService : IReporteSolicitudCompromisoService
    {
        private readonly IPreSolModificacionRepository _preSolModificacionRepository;
        private readonly IPreSolModificacionService _preSolModificacionService;
        private readonly IPrePucSolicitudModificacionService _prePucSolicitudModificacionService;
        private readonly IPRE_V_SALDOSRepository _pRE_V_SALDOSRepository;
        private readonly IPreAsignacionService _preAsignacionService;
        private readonly IConfiguration _configuration;

        public ReporteSolicitudCompromisoService(IPreSolModificacionRepository preSolModificacionRepository,
                                                                 IPreSolModificacionService preSolModificacionService,
                                                                 IPrePucSolicitudModificacionService prePucSolicitudModificacionService,
                                                                 IPRE_V_SALDOSRepository pRE_V_SALDOSRepository,
                                                                  IPreAsignacionService preAsignacionService,
                                                                 IConfiguration configuration)
        {

            _preSolModificacionRepository = preSolModificacionRepository;
            _preSolModificacionService = preSolModificacionService;
            _prePucSolicitudModificacionService = prePucSolicitudModificacionService;
            _pRE_V_SALDOSRepository = pRE_V_SALDOSRepository;
            _preAsignacionService = preAsignacionService;
            _configuration = configuration;
        }

        //public async Task<ResultDto<List<DetalleReporteSolicitudModificacionDto>>> GetDataSolicitudModificacion(int codigoSolModificacion)
        //{
        //    ResultDto<List<DetalleReporteSolicitudModificacionDto>> result = new ResultDto<List<DetalleReporteSolicitudModificacionDto>>(null);
        //    try
        //    {



        //        var solicitudModificacion = await _preSolModificacionService.GetByCodigoSolicitud(codigoSolModificacion);
        //        if (solicitudModificacion != null)
        //        {
        //            GeneralReporteSolicitudModificacionDto generalDto = new GeneralReporteSolicitudModificacionDto();
        //            solicitudModificacion.Data.CodigoSolModificacion = generalDto.CodigoSolModificacion;




        //            var pucSolModificacion = _prePucSolicitudModificacionRepository.GetAllByCodigoSolicitud(generalDto.CodigoSolModificacion);
        //            if (pucSolModificacion  !=  null)
        //            {
        //                List<DetalleReporteSolicitudModificacionDto> lisDto = new List<DetalleReporteSolicitudModificacionDto>();
        //                foreach (var item in lisDto)
        //                {

        //                    var dto = item;
        //                    lisDto.Add(dto);

        //                }

        //                result.Data = lisDto;

        //                result.IsValid = true;
        //                result.Message = "";
        //            }

        //            else
        //            {
        //                result.Data = null;
        //                result.IsValid = true;
        //                result.Message = " No existen Datos";

        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Data = null;
        //        result.IsValid = false;
        //        result.Message = ex.Message;

        //    }

        //    return result;

        //}

        public async Task<AdmSolicitudesResponseDto> GenerateData(int codigoSolicitud)
        {
            AdmSolicitudesResponseDto result = new AdmSolicitudesResponseDto();


            var solicitud = await GenerateDatasolicitud(codigoSolicitud);
            var detalle = await GenerateDataDetalle(codigoSolicitud);


            result.CodigoSolicitud = codigoSolicitud;
            result.TipoSolicitudId = codigoSolicitud;


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

        public async Task<AdmSolicitudesResponseDto> GenerateDatasolicitud(int codigoSolicitud)
        {

            AdmSolicitudesResponseDto result = new AdmSolicitudesResponseDto();
            //var solicitudModificacion = await _preSolModificacionService.GetByCodigoSolicitud(codigoSolModificacion);
            //if(solicitudModificacion.Data != null) 
            //{
            //    result.CodigoSolModificacion = solicitudModificacion.Data.CodigoSolModificacion;
            //    result.TipoModificacionId = solicitudModificacion.Data.TipoModificacionId;
            //    result.DescripcionTipoModificacion = solicitudModificacion.Data.DescripcionTipoModificacion;
            //    result.Descontar = solicitudModificacion.Data.Descontar;
            //    result.Aportar = solicitudModificacion.Data.Aportar;
            //    result.OrigenPreSaldo = solicitudModificacion.Data.OrigenPreSaldo;
            //    result.FechaSolicitud = solicitudModificacion.Data.FechaSolicitud;
            //    result.FechaSolicitudString = solicitudModificacion.Data.FechaSolicitud.ToString("u");
            //    FechaDto fechaSolicitudObj = GetFechaDto(solicitudModificacion.Data.FechaSolicitud);
            //    result.FechaSolicitudObj = (FechaDto)fechaSolicitudObj;
            //    result.Ano = solicitudModificacion.Data.Ano;
            //    result.NumeroSolModificacion = solicitudModificacion.Data.NumeroSolModificacion;
            //    result.CodigoOficio = solicitudModificacion.Data.CodigoOficio;
            //    result.CodigoSolicitante = solicitudModificacion.Data.CodigoSolicitante;
            //    result.Motivo = solicitudModificacion.Data.Motivo;
            //    result.Status = solicitudModificacion.Data.Status;
            //    result.DescripcionEstatus = solicitudModificacion.Data.DescripcionEstatus;
            //    result.NumeroCorrelativo = solicitudModificacion.Data.NumeroCorrelativo;
            //    result.CodigoPresupuesto = solicitudModificacion.Data.CodigoPresupuesto;
            //    result.StatusProceso = solicitudModificacion.Data.StatusProceso;
            //}

            return result;


        }

        public async Task<List<AdmSolCompromisoResponseDto>> GenerateDataDetalle(int codigoSolCompromiso)
        {
            List<AdmSolCompromisoResponseDto> result = new List<AdmSolCompromisoResponseDto>();

        AdmSolCompromisoResponseDto filter = new AdmSolCompromisoResponseDto();

            filter.CodigoSolCompromiso = codigoSolCompromiso;





            //var pucSolModificacion = await _prePucSolicitudModificacionService.GetAllByCodigoSolicitud(filter);

            //if(pucSolModificacion.Data == null) 
            //{
            //    return null;
            //}
            //var listDto = pucSolModificacion.Data.Where(x => x.DePara == dePara).ToList();



            //    if (listDto.Count > 0)
            //    {


            //        foreach (var item in listDto)
            //        {

            //            DetalleReporteSolicitudModificacionDto resultItem = new DetalleReporteSolicitudModificacionDto();





            //            resultItem.CodigoPucSolModificacion = item.CodigoPucSolModificacion;
            //            resultItem.CodigoSolModificacion = item.CodigoSolModificacion;
            //            resultItem.CodigoSaldo = item.CodigoSaldo;
            //            resultItem.FinanciadoId = item.FinanciadoId;
            //            resultItem.DescripcionFinanciado = item.DescripcionFinanciado;
            //            resultItem.CodigoFinanciado = item.CodigoFinanciado;
            //            resultItem.CodigoIcp = item.CodigoIcp;
            //            resultItem.CodigoIcpConcat = item.CodigoIcpConcat;
            //            resultItem.DenominacionIcp = item.DenominacionIcp;
            //            resultItem.CodigoPuc = item.CodigoPuc;
            //            resultItem.CodigoPucConcat = item.CodigoPucConcat;
            //            resultItem.DenominacionPuc = item.DenominacionPuc;
            //            resultItem.Monto = item.Monto;
            //            resultItem.Presupuestado = 0;
            //            resultItem.MontoModificado = 0;
            //            resultItem.Disponible = 0;
            //           var saldos = await _pRE_V_SALDOSRepository.GetByCodigo(item.CodigoSaldo);
            //            if(saldos != null) 
            //            {

            //                resultItem.Presupuestado = saldos.PRESUPUESTADO;
            //                resultItem.MontoModificado = saldos.MODIFICADO;
            //                resultItem.Disponible = saldos.DISPONIBLE;
            //            }

            //            var totalDesembolso = await _preAsignacionService.GetTotalAsignacionByIcpPuc(item.CodigoPresupuesto,item.CodigoIcp, item.CodigoPuc);
            //            resultItem.TotalDesembolso = totalDesembolso;
            //            resultItem.MontoAnulado = item.MontoAnulado;
            //            resultItem.Descontar = item.Descontar;
            //            resultItem.Aportar = item.Aportar;
            //            resultItem.DePara = item.DePara;
            //            resultItem.MontoAnulado = item.MontoAnulado;


            //            result.Add(resultItem);

            //}

            return result;
        }


    }

    
 
//    public async Task<string> ReportData(int codigoSolicitud)
//    {
//        AdmSolicitudesResponseDto filter = new AdmSolicitudesResponseDto();

//        ////filter.CodigoSolModificacion = codigoSolModificacion;


//        ////var settings = _configuration.GetSection("Settings").Get<Settings>();
//        var result = "No Data";
//        ////var pathLogo = @settings.BmFiles + "LogoIzquierda.jpeg";
//        //var fileName = $"ReporteSolicitudModificacionPresupuestaria-{filter.CodigoSolicitud}.pdf";
//        ////var filePath = $"{@settings.ExcelFiles}/{fileName}.pdf";


//        if (filter == null)
//        {
//            return null;
//        }



//        ////var reporte = await (filter.CodigoSolicitud);
//        ////if (reporte != null)
//        ////{




//        ////    var SolicitudCompromiso = await GenerateDatasolicitud(filter.CodigoSolicitud);

//        ////    if (reporte == null)
//        ////    {
//        ////        return "No Data";
//        ////    }
//        ////    else
//        ////    {


//        ////        filePath = fileName;


//        result = fileName;
//        ////        ////var document = new ReporteSolicitudCompromisoDocument(reporte, pathLogo);
//        ////        ////document.GeneratePdf(filePath);
//    }

//        return result;



//    }
//}

