using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;
using Convertidor.Services.Sis;

namespace Convertidor.Data.Repository.Rh
{
	public class RhHRetencionesFjpService : IRhHRetencionesFjpService
    {
        
   
        private readonly IRhHRetencionesFjpRepository _repository;
        private readonly IOssConfigServices _ossConfigService;



        public RhHRetencionesFjpService(IRhHRetencionesFjpRepository repository, IOssConfigServices ossConfigService)
        {
            _repository = repository;
            _ossConfigService = ossConfigService;
        }
       
        public async Task<List<RhTmpRetencionesFjpDto>> GetRetencionesHFjp(FilterRetencionesDto filter)
        {
            try
            {

                
                var retenciones = await _repository.GetByTipoNominaFechaDesdeFachaHasta(filter.TipoNomina,filter.FechaDesde,filter.FechaHasta);

                //TODO
                // LLENAR HISTORICO A PARTIR DEL TEMPORA

                var result = await MapListRhTmpRetencionesFJPDto(retenciones);


                return (List<RhTmpRetencionesFjpDto>)result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public async  Task<RhTmpRetencionesFjpDto> MapRhTmpRetencionesFjpDto(RH_H_RETENCIONES_FJP entity)
        {


                RhTmpRetencionesFjpDto itemResult = new RhTmpRetencionesFjpDto();
                itemResult.CodigoRetencionAporte = entity.CODIGO_RETENCION_APORTE;
                itemResult.Secuencia = entity.SECUENCIA;
                itemResult.UnidadEjecutora = entity.UNIDAD_EJECUTORA;
                itemResult.CedulaTexto = entity.CEDULATEXTO;
                itemResult.NombresApellidos = entity.NOMBRES_APELLIDOS;
                itemResult.DescripcionCargo = entity.DESCRIPCION_CARGO;
                itemResult.FechaIngreso = entity.FECHA_INGRESO;
                itemResult.MontoFjpTrabajador = entity.MONTO_FJP_TRABAJADOR;
                itemResult.MontoFjpPatrono = entity.MONTO_FJP_PATRONO;
                itemResult.MontoTotalRetencion = entity.MONTO_TOTAL_RETENCION;
                itemResult.FechaNomina = entity.FECHA_NOMINA;
                itemResult.SiglasTipoNomina = entity.SIGLAS_TIPO_NOMINA;
                itemResult.FechaDesde = entity.FECHA_DESDE;
                itemResult.FechaHasta = entity.FECHA_HASTA;
                itemResult.CodigoTipoNomina = entity.CODIGO_TIPO_NOMINA;


            return itemResult;

        }

        public async  Task<List<RhTmpRetencionesFjpDto>> MapListRhTmpRetencionesFJPDto(List<RH_H_RETENCIONES_FJP> entities)
        {
            List<RhTmpRetencionesFjpDto> result = new List<RhTmpRetencionesFjpDto>();
           
            if(entities!= null)
            {
                foreach (var item in entities)
                {

                    RhTmpRetencionesFjpDto itemResult = new RhTmpRetencionesFjpDto();

                    itemResult = await MapRhTmpRetencionesFjpDto(item);

                    result.Add(itemResult);
                }
            }
            

           
            return result;



        }



        public async Task<ResultDto<string>> Create (List<RH_H_RETENCIONES_FJP> entities)
        {

            ResultDto<string> result = new ResultDto<string>("");

            try
            {

                foreach (var item in entities)
                {
                    RH_H_RETENCIONES_FJP entity = new RH_H_RETENCIONES_FJP();
                    var nextId = await _repository.GetNextKey();
                    entity.CODIGO_RETENCION_APORTE = nextId;
                    entity.SECUENCIA= item.SECUENCIA;
                    entity.UNIDAD_EJECUTORA = item.UNIDAD_EJECUTORA;
                    entity.CEDULATEXTO = item.CEDULATEXTO;
                    entity.NOMBRES_APELLIDOS = item.NOMBRES_APELLIDOS;
                    entity.DESCRIPCION_CARGO = item.DESCRIPCION_CARGO;
                    entity.FECHA_INGRESO = item.FECHA_INGRESO;
                    entity.MONTO_FJP_TRABAJADOR = item.MONTO_FJP_TRABAJADOR;
                    entity.MONTO_FJP_PATRONO = item.MONTO_FJP_PATRONO;
                    entity.MONTO_TOTAL_RETENCION = item.MONTO_TOTAL_RETENCION;
                    entity.FECHA_NOMINA = item.FECHA_NOMINA;
                    entity.SIGLAS_TIPO_NOMINA = item.SIGLAS_TIPO_NOMINA;
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
                result.Message = "Historico Fjp Actualizado";

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

