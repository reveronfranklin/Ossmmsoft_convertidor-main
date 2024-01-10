using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;
using Convertidor.Services.Sis;

namespace Convertidor.Data.Repository.Rh
{
	public class RhHRetencionesSindService : IRhHRetencionesSindService
    {
        
   
        private readonly IRhHRetencionesSindRepository _repository;
        private readonly IOssConfigServices _ossConfigService;



        public RhHRetencionesSindService(IRhHRetencionesSindRepository repository, IOssConfigServices ossConfigService)
        {
            _repository = repository;
            _ossConfigService = ossConfigService;
        }
       
        public async Task<List<RhTmpRetencionesSindDto>> GetRetencionesHSind(FilterRetencionesDto filter)
        {
            try
            {

                
                var retenciones = await _repository.GetByTipoNominaFechaDesdeFachaHasta(filter.TipoNomina,filter.FechaDesde,filter.FechaHasta);

                //TODO
                // LLENAR HISTORICO A PARTIR DEL TEMPORA

                var result = await MapListRhTmpRetencionesSindDto(retenciones);


                return (List<RhTmpRetencionesSindDto>)result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public async  Task<RhTmpRetencionesSindDto> MapRhTmpRetencionesSindDto(RH_H_RETENCIONES_SIND entity)
        {


                RhTmpRetencionesSindDto itemResult = new RhTmpRetencionesSindDto();
                itemResult.CodigoRetencionAporte = entity.CODIGO_RETENCION_APORTE;
                itemResult.Secuencia = entity.SECUENCIA;
                itemResult.UnidadEjecutora = entity.UNIDAD_EJECUTORA;
                itemResult.CedulaTexto = entity.CEDULATEXTO;
                itemResult.NombresApellidos = entity.NOMBRES_APELLIDOS;
                itemResult.DescripcionCargo = entity.DESCRIPCION_CARGO;
                itemResult.FechaIngreso = entity.FECHA_INGRESO;
                itemResult.MontoSindTrabajador = entity.MONTO_SIND_TRABAJADOR;
                itemResult.MontoSindPatrono = entity.MONTO_SIND_PATRONO;
                itemResult.MontoTotalRetencion = entity.MONTO_TOTAL_RETENCION;
                itemResult.FechaNomina = entity.FECHA_NOMINA;
                itemResult.SiglasTipoNomina = entity.SIGLAS_TIPO_NOMINA;
                itemResult.FechaDesde = entity.FECHA_DESDE;
                itemResult.FechaHasta = entity.FECHA_HASTA;
                itemResult.CodigoTipoNomina = entity.CODIGO_TIPO_NOMINA;


            return itemResult;

        }

        public async  Task<List<RhTmpRetencionesSindDto>> MapListRhTmpRetencionesSindDto(List<RH_H_RETENCIONES_SIND> entities)
        {
            List<RhTmpRetencionesSindDto> result = new List<RhTmpRetencionesSindDto>();
           
            if(entities!= null)
            {
                foreach (var item in entities)
                {

                    RhTmpRetencionesSindDto itemResult = new RhTmpRetencionesSindDto();

                    itemResult = await MapRhTmpRetencionesSindDto(item);

                    result.Add(itemResult);
                }
            }
            

           
            return result;



        }



        public async Task<ResultDto<string>> Create (List<RH_H_RETENCIONES_SIND> entities)
        {

            ResultDto<string> result = new ResultDto<string>("");

            try
            {

                foreach (var item in entities)
                {
                    RH_H_RETENCIONES_SIND entity = new RH_H_RETENCIONES_SIND();
                    var nextId = await _repository.GetNextKey();
                    entity.CODIGO_RETENCION_APORTE = nextId;
                    entity.SECUENCIA= item.SECUENCIA;
                    entity.UNIDAD_EJECUTORA = item.UNIDAD_EJECUTORA;
                    entity.CEDULATEXTO = item.CEDULATEXTO;
                    entity.NOMBRES_APELLIDOS = item.NOMBRES_APELLIDOS;
                    entity.DESCRIPCION_CARGO = item.DESCRIPCION_CARGO;
                    entity.FECHA_INGRESO = item.FECHA_INGRESO;
                    entity.MONTO_SIND_TRABAJADOR = item.MONTO_SIND_TRABAJADOR;
                    entity.MONTO_SIND_PATRONO = item.MONTO_SIND_PATRONO;
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
                result.Message = "Historico Sind Actualizado";

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

