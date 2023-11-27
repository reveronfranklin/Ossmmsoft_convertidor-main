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
	public class RhTmpRetencionesCahService : IRhTmpRetencionesCahService
    {
        
   
        private readonly IRhTmpRetencionesCahRepository _repository;
        private readonly IOssConfigServices _ossConfigService;
        private readonly IRhHRetencionesCahService _rrhservice;
        private readonly IConfiguration _configuration;

        public RhTmpRetencionesCahService(IRhTmpRetencionesCahRepository repository,
                                                IOssConfigServices ossConfigService,
                                                IRhHRetencionesCahService rrhservice,
                                                IConfiguration configuration)
        {
            _repository = repository;
            _ossConfigService = ossConfigService;
            _rrhservice = rrhservice;
            _configuration = configuration;
        }

        public async Task<ResultDto<List<RhTmpRetencionesCahDto>>> GetRetencionesCah(FilterRetencionesDto filter)
        {
            ResultDto<List<RhTmpRetencionesCahDto>> result = new ResultDto<List<RhTmpRetencionesCahDto>>(null);
            try
            {
                var historico = await _rrhservice.GetRetencionesHCah(filter);
                if (historico.Count == 0)
                {
                    int procesoId = 0;
                    procesoId = await _ossConfigService.GetNextByClave("CONSECUTIVO_RETENCIONES");

                    await _repository.Add(procesoId, filter.TipoNomina, filter.FechaDesde, filter.FechaHasta);
                    var retenciones = await _repository.GetByProcesoId(procesoId);
                    if (retenciones != null)
                    {
                        var listRetenciones = MapRetencionesCahTmpH(retenciones);
                        var created = await _rrhservice.Create(listRetenciones);
                        await _repository.Delete(procesoId);

                    }
                    result.Data = await MapListRhTmpRetencionesCahDto(retenciones);
                }
                else
                {
                    result.Data = historico;

                }
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





                var fileName = $"RetencionesCAH desde {desdeFilter} Hasta {hastaFilter} Tipo Nomina {filter.TipoNomina}.xlsx";
                string newFile = Path.Combine(Directory.GetCurrentDirectory(), ruta, fileName);


                mapper.Save(newFile, result.Data, $"RetencionesCAH", true);

                result.IsValid = true;
                result.Message = "";
                result.LinkData = $"/ExcelFiles/{fileName}";
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

        public List<RH_H_RETENCIONES_CAH> MapRetencionesCahTmpH(List<RH_TMP_RETENCIONES_CAH> retenciones)
        {
            List<RH_H_RETENCIONES_CAH> result = new List<RH_H_RETENCIONES_CAH>();

            foreach (var retencion in retenciones)
            {
                RH_H_RETENCIONES_CAH resultItem = new RH_H_RETENCIONES_CAH();

                resultItem.CODIGO_RETENCION_APORTE = retencion.CODIGO_RETENCION_APORTE;
                resultItem.SECUENCIA = retencion.SECUENCIA;
                resultItem.UNIDAD_EJECUTORA = retencion.UNIDAD_EJECUTORA;
                resultItem.CEDULATEXTO = retencion.CEDULATEXTO;
                resultItem.NOMBRES_APELLIDOS = retencion.NOMBRES_APELLIDOS;
                resultItem.DESCRIPCION_CARGO = retencion.DESCRIPCION_CARGO;
                resultItem.FECHA_INGRESO = retencion.FECHA_INGRESO;
                resultItem.MONTO_CAH_TRABAJADOR = retencion.MONTO_CAH_TRABAJADOR;
                resultItem.MONTO_CAH_PATRONO = retencion.MONTO_CAH_PATRONO;
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
        public async  Task<RhTmpRetencionesCahDto> MapRhTmpRetencionesCahDto(RH_TMP_RETENCIONES_CAH entity)
        {


                RhTmpRetencionesCahDto itemResult = new RhTmpRetencionesCahDto();
                itemResult.CodigoRetencionAporte = entity.CODIGO_RETENCION_APORTE;
                itemResult.Secuencia = entity.SECUENCIA;
                itemResult.UnidadEjecutora = entity.UNIDAD_EJECUTORA;
                itemResult.CedulaTexto = entity.CEDULATEXTO;
                itemResult.NombresApellidos = entity.NOMBRES_APELLIDOS;
                itemResult.DescripcionCargo = entity.DESCRIPCION_CARGO;
                itemResult.FechaIngreso = entity.FECHA_INGRESO;
                itemResult.MontoCahTrabajador = entity.MONTO_CAH_TRABAJADOR;
                itemResult.MontoCahPatrono = entity.MONTO_CAH_PATRONO;
                itemResult.MontoTotalRetencion = entity.MONTO_TOTAL_RETENCION;
                itemResult.FechaNomina = entity.FECHA_NOMINA;
                itemResult.SiglasTipoNomina = entity.SIGLAS_TIPO_NOMINA;
                itemResult.FechaDesde = entity.FECHA_DESDE;
                itemResult.FechaHasta = entity.FECHA_HASTA;
                itemResult.CodigoTipoNomina = entity.CODIGO_TIPO_NOMINA;


            return itemResult;

        }

        public async  Task<List<RhTmpRetencionesCahDto>> MapListRhTmpRetencionesCahDto(List<RH_TMP_RETENCIONES_CAH> entities)
        {
            List<RhTmpRetencionesCahDto> result = new List<RhTmpRetencionesCahDto>();
           
            
            foreach (var item in entities)
            {

                RhTmpRetencionesCahDto itemResult = new RhTmpRetencionesCahDto();

                itemResult = await MapRhTmpRetencionesCahDto(item);
               
                result.Add(itemResult);
            }
            return result;



        }

        
   

          
    }
}

