﻿using Convertidor.Services.Sis;

namespace Convertidor.Data.Repository.Rh
{
	public class RhHRetencionesSsoService : IRhHRetencionesSsoService
    {
        
   
        private readonly IRhHRetencionesSsoRepository _repository;
        private readonly IOssConfigServices _ossConfigService;



        public RhHRetencionesSsoService(IRhHRetencionesSsoRepository repository, IOssConfigServices ossConfigService)
        {
            _repository = repository;
            _ossConfigService = ossConfigService;
        }
       
        public async Task<List<RhTmpRetencionesSsoDto>> GetRetencionesHSso(FilterRetencionesDto filter)
        {
            try
            {

                
                var retenciones = await _repository.GetByTipoNominaFechaDesdeFachaHasta(filter.TipoNomina,filter.FechaDesde,filter.FechaHasta);

                //TODO
                // LLENAR HISTORICO A PARTIR DEL TEMPORA

                var result = await MapListRhHRetencionesSsoDto(retenciones);


                return (List<RhTmpRetencionesSsoDto>)result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public async  Task<RhTmpRetencionesSsoDto> MapRhHRetencionesSsoDto(RH_H_RETENCIONES_SSO entity)
        {


                RhTmpRetencionesSsoDto itemResult = new RhTmpRetencionesSsoDto();
                itemResult.CodigoRetencionAporte = entity.CODIGO_RETENCION_APORTE;
                itemResult.Secuencia = entity.SECUENCIA;
                itemResult.UnidadEjecutora = entity.UNIDAD_EJECUTORA;
                itemResult.CedulaTexto = entity.CEDULATEXTO;
                itemResult.NombresApellidos = entity.NOMBRES_APELLIDOS;
                itemResult.DescripcionCargo = entity.DESCRIPCION_CARGO;
                itemResult.FechaIngreso = entity.FECHA_INGRESO;
                itemResult.MontoSsoTrabajador = entity.MONTO_SSO_TRABAJADOR;
                itemResult.MontoRpeTrabajador = entity.MONTO_RPE_TRABAJADOR;
                itemResult.MontoSsoPatrono = entity.MONTO_SSO_PATRONO;
                itemResult.MontoTotalRetencion = entity.MONTO_TOTAL_RETENCION;
                itemResult.FechaNomina = entity.FECHA_NOMINA;
                itemResult.SiglasTipoNomina = entity.SIGLAS_TIPO_NOMINA;
                itemResult.FechaDesde = entity.FECHA_DESDE;
                itemResult.FechaHasta = entity.FECHA_HASTA;
                itemResult.CodigoTipoNomina = entity.CODIGO_TIPO_NOMINA;
                

                return itemResult;

        }

        public async  Task<List<RhTmpRetencionesSsoDto>> MapListRhHRetencionesSsoDto(List<RH_H_RETENCIONES_SSO> entities)
        {
            List<RhTmpRetencionesSsoDto> result = new List<RhTmpRetencionesSsoDto>();
           
            if(entities!= null)
            {
                foreach (var item in entities)
                {

                    RhTmpRetencionesSsoDto itemResult = new RhTmpRetencionesSsoDto();

                    itemResult = await MapRhHRetencionesSsoDto(item);

                    result.Add(itemResult);
                }
            }
            

           
            return result;



        }



        public async Task<ResultDto<string>> Create (List<RH_H_RETENCIONES_SSO> entities)
        {

            ResultDto<string> result = new ResultDto<string>("");

            try
            {

                foreach (var item in entities)
                {
                    RH_H_RETENCIONES_SSO entity = new RH_H_RETENCIONES_SSO();
                    var nextId = await _repository.GetNextKey();
                    entity.CODIGO_RETENCION_APORTE = nextId;
                    entity.SECUENCIA= item.SECUENCIA;
                    entity.UNIDAD_EJECUTORA = item.UNIDAD_EJECUTORA;
                    entity.CEDULATEXTO = item.CEDULATEXTO;
                    entity.NOMBRES_APELLIDOS = item.NOMBRES_APELLIDOS;
                    entity.DESCRIPCION_CARGO = item.DESCRIPCION_CARGO;
                    entity.FECHA_INGRESO = item.FECHA_INGRESO;
                    entity.MONTO_SSO_TRABAJADOR = item.MONTO_SSO_TRABAJADOR;
                    entity.MONTO_RPE_TRABAJADOR = item.MONTO_RPE_TRABAJADOR;
                    entity.MONTO_SSO_PATRONO = item.MONTO_RPE_PATRONO;
                    entity.MONTO_RPE_PATRONO = item.MONTO_RPE_PATRONO;
                    entity.MONTO_TOTAL_RETENCION= item.MONTO_TOTAL_RETENCION;
                    entity.FECHA_NOMINA= item.FECHA_NOMINA;
                    entity.SIGLAS_TIPO_NOMINA = item.SIGLAS_TIPO_NOMINA;
                    entity.PROCESO_ID = item.PROCESO_ID;
                    entity.FECHA_DESDE = item.FECHA_DESDE;
                    entity.FECHA_HASTA = item.FECHA_HASTA;
                    entity.CODIGO_TIPO_NOMINA = item.CODIGO_TIPO_NOMINA;
                    


                    var created = await _repository.Add(entity);
                }

                result.Data = "";
                result.IsValid = true;
                result.Message = "Historico Sso Actualizado";

                return result;

            }
            catch (Exception ex)
            {
                result.Data = "";
                result.IsValid = false;
                result.Message = ex.Message;
            }

            return result;
        }


    }


}

