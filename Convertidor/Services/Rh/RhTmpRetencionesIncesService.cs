using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;
using Convertidor.Services.Sis;
using Ganss.Excel;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
    public class RhTmpRetencionesIncesService : IRhTmpRetencionesIncesService
    {


        private readonly IRhTmpRetencionesIncesRepository _repository;
        private readonly IOssConfigServices _ossConfigService;
        private readonly IRhHRetencionesIncesService _rrhservice;
        private readonly IConfiguration _configuration;

        public RhTmpRetencionesIncesService(IRhTmpRetencionesIncesRepository repository,
                                            IOssConfigServices ossConfigService,
                                            IRhHRetencionesIncesService rrhservice,
                                            IConfiguration configuration)
        {
            _repository = repository;
            _ossConfigService = ossConfigService;
            _rrhservice = rrhservice;
            _configuration = configuration;
        }

        public async Task<ResultDto<List<RhTmpRetencionesIncesDto>>> GetRetencionesInces(FilterRetencionesDto filter)
        {
            ResultDto<List<RhTmpRetencionesIncesDto>> result = new ResultDto<List<RhTmpRetencionesIncesDto>>(null);
            try
            {
                var historico = await _rrhservice.GetRetencionesHInces(filter);
                if (historico.Count == 0)
                {
                    int procesoId = 0;
                    procesoId = await _ossConfigService.GetNextByClave("CONSECUTIVO_RETENCIONES");

                    await _repository.Add(procesoId, filter.TipoNomina, filter.FechaDesde, filter.FechaHasta);
                    var retenciones = await _repository.GetByProcesoId(procesoId);
                    if (retenciones.Count > 0)
                    {
                        var listRetenciones = MapRetencionesIncesTmpH(retenciones);
                        var created = await _rrhservice.Create(listRetenciones);
                        await _repository.Delete(procesoId);

                    }
                    result.Data = await MapListRhTmpRetencionesIncesDto(retenciones);
                }
                else
                {
                    result.Data = historico;

                }
                var linkData = $"";
                if (result.Data.Count > 0)
                {
                    ExcelMapper mapper = new ExcelMapper();


                    var settings = _configuration.GetSection("Settings").Get<Settings>();


                    var ruta = @settings.ExcelFiles;  //@"/Users/freveron/Documents/MM/App/full-version/public/ExcelFiles";
                    DateTime desde = Convert.ToDateTime(filter.FechaDesde);
                    var mesString = "00" + desde.Month.ToString();
                    var diaString = "00" + desde.Day.ToString();
                    string mes = mesString.Substring(mesString.Length - 2, 2);
                    string dia = diaString.Substring(diaString.Length - 2, 2);
                    var desdeFilter = desde.Year + mes + dia;

                    DateTime hasta = Convert.ToDateTime(filter.FechaHasta);
                    var mesHastaString = "00" + hasta.Month.ToString();
                    var diaHastaString = "00" + hasta.Day.ToString();
                    string mesHasta = mesHastaString.Substring(mesHastaString.Length - 2, 2);
                    string diaHasta = diaHastaString.Substring(diaHastaString.Length - 2, 2);
                    var hastaFilter = hasta.Year + mesHasta + diaHasta;
                    var fileName = $"RetencionesInce desde {desdeFilter} Hasta {hastaFilter} Tipo Nomina {filter.TipoNomina}.xlsx";
                    string newFile = Path.Combine(Directory.GetCurrentDirectory(), ruta, fileName);


                    mapper.Save(newFile, result.Data, $"RetencionesCAH", true);
                    linkData = $"/ExcelFiles/{fileName}";

                }
                result.IsValid = true;
                result.Message = "";
                result.LinkData = linkData;
                return result;

            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = true;
                result.Message = "";
                result.LinkData = "";
                return result;
            }

        }

        public List<RH_H_RETENCIONES_INCES> MapRetencionesIncesTmpH(List<RH_TMP_RETENCIONES_INCES> retenciones)
        {
            List<RH_H_RETENCIONES_INCES> result = new List<RH_H_RETENCIONES_INCES>();

            foreach (var retencion in retenciones)
            {
                RH_H_RETENCIONES_INCES resultItem = new RH_H_RETENCIONES_INCES();

                resultItem.CODIGO_RETENCION_APORTE = retencion.CODIGO_RETENCION_APORTE;
                resultItem.SECUENCIA = retencion.SECUENCIA;
                resultItem.UNIDAD_EJECUTORA = retencion.UNIDAD_EJECUTORA;
                resultItem.CEDULATEXTO = retencion.CEDULATEXTO;
                resultItem.NOMBRES_APELLIDOS = retencion.NOMBRES_APELLIDOS;
                resultItem.DESCRIPCION_CARGO = retencion.DESCRIPCION_CARGO;
                resultItem.FECHA_INGRESO = retencion.FECHA_INGRESO;
                resultItem.MONTO_INCES_TRABAJADOR = retencion.MONTO_INCES_TRABAJADOR;
                resultItem.MONTO_INCES_PATRONO = retencion.MONTO_INCES_PATRONO;
                resultItem.MONTO_TOTAL_RETENCION = retencion.MONTO_TOTAL_RETENCION;
                resultItem.FECHA_NOMINA = retencion.FECHA_NOMINA;
                resultItem.SIGLAS_TIPO_NOMINA = retencion.SIGLAS_TIPO_NOMINA;
                resultItem.PROCESO_ID = retencion.PROCESO_ID;
                resultItem.FECHA_DESDE = retencion.FECHA_DESDE;
                resultItem.FECHA_HASTA = retencion.FECHA_HASTA;
                resultItem.CODIGO_TIPO_NOMINA = retencion.CODIGO_TIPO_NOMINA;



                result.Add(resultItem);



            }

            return result;

        }

        public async Task<RhTmpRetencionesIncesDto> MapRhTmpRetencionesIncesDto(RH_TMP_RETENCIONES_INCES entity)
        {


            RhTmpRetencionesIncesDto itemResult = new RhTmpRetencionesIncesDto();
            itemResult.CodigoRetencionAporte = entity.CODIGO_RETENCION_APORTE;
            itemResult.Secuencia = entity.SECUENCIA;
            itemResult.UnidadEjecutora = entity.UNIDAD_EJECUTORA;
            itemResult.CedulaTexto = entity.CEDULATEXTO;
            itemResult.NombresApellidos = entity.NOMBRES_APELLIDOS;
            itemResult.DescripcionCargo = entity.DESCRIPCION_CARGO;
            itemResult.FechaIngreso = entity.FECHA_INGRESO;
            itemResult.MontoIncesTrabajador = entity.MONTO_INCES_TRABAJADOR;
            itemResult.MontoIncesPatrono = entity.MONTO_INCES_PATRONO;
            itemResult.MontoTotalRetencion = entity.MONTO_TOTAL_RETENCION;
            itemResult.FechaNomina = entity.FECHA_NOMINA;
            itemResult.SiglasTipoNomina = entity.SIGLAS_TIPO_NOMINA;
            itemResult.FechaDesde = entity.FECHA_DESDE;
            itemResult.FechaHasta = entity.FECHA_HASTA;
            itemResult.CodigoTipoNomina = entity.CODIGO_TIPO_NOMINA;

            return itemResult;

        }

        public async Task<List<RhTmpRetencionesIncesDto>> MapListRhTmpRetencionesIncesDto(List<RH_TMP_RETENCIONES_INCES> entities)
        {
            List<RhTmpRetencionesIncesDto> result = new List<RhTmpRetencionesIncesDto>();

            if (entities != null)
            {
                foreach (var item in entities)
                {

                    RhTmpRetencionesIncesDto itemResult = new RhTmpRetencionesIncesDto();

                    itemResult = await MapRhTmpRetencionesIncesDto(item);

                    result.Add(itemResult);
                }
            }

            return result;



        }





    }
}