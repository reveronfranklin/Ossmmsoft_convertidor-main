using Convertidor.Services.Sis;

namespace Convertidor.Data.Repository.Rh
{
	public class RhHRetencionesFaovService : IRhHRetencionesFaovService
    {
        
   
        private readonly IRhHRetencionesFaovRepository _repository;
        private readonly IOssConfigServices _ossConfigService;



        public RhHRetencionesFaovService(IRhHRetencionesFaovRepository repository, IOssConfigServices ossConfigService)
        {
            _repository = repository;
            _ossConfigService = ossConfigService;
        }
       
        public async Task<List<RhTmpRetencionesFaovDto>> GetRetencionesHFaov(FilterRetencionesDto filter)
        {
            try
            {

                
                var retenciones = await _repository.GetByTipoNominaFechaDesdeFachaHasta(filter.TipoNomina,filter.FechaDesde,filter.FechaHasta);

                //TODO
                // LLENAR HISTORICO A PARTIR DEL TEMPORA

                var result = await MapListRhTmpRetencionesFaovDto(retenciones);


                return (List<RhTmpRetencionesFaovDto>)result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public async  Task<RhTmpRetencionesFaovDto> MapRhTmpRetencionesFaovDto(RH_H_RETENCIONES_FAOV entity)
        {


                RhTmpRetencionesFaovDto itemResult = new RhTmpRetencionesFaovDto();
                itemResult.CodigoRetencionAporte = entity.CODIGO_RETENCION_APORTE;
                itemResult.Secuencia = entity.SECUENCIA;
                itemResult.UnidadEjecutora = entity.UNIDAD_EJECUTORA;
                itemResult.CedulaTexto = entity.CEDULATEXTO;
                itemResult.NombresApellidos = entity.NOMBRES_APELLIDOS;
                itemResult.DescripcionCargo = entity.DESCRIPCION_CARGO;
                itemResult.FechaIngreso = entity.FECHA_INGRESO;
                itemResult.MontoFaovTrabajador = entity.MONTO_FAOV_TRABAJADOR;
                itemResult.MontoFaovPatrono = entity.MONTO_FAOV_PATRONO;
                itemResult.MontoTotalRetencion = entity.MONTO_TOTAL_RETENCION;
                itemResult.FechaNomina = entity.FECHA_NOMINA;
                itemResult.SiglasTipoNomina = entity.SIGLAS_TIPO_NOMINA;
                itemResult.RegistroConcat = entity.REGISTRO_CONCAT;
                itemResult.FechaDesde = entity.FECHA_DESDE;
                itemResult.FechaHasta = entity.FECHA_HASTA;
                itemResult.CodigoTipoNomina = entity.CODIGO_TIPO_NOMINA;
                itemResult.RegistroConcat = entity.REGISTRO_CONCAT;


            return itemResult;

        }

        public async  Task<List<RhTmpRetencionesFaovDto>> MapListRhTmpRetencionesFaovDto(List<RH_H_RETENCIONES_FAOV> entities)
        {
            List<RhTmpRetencionesFaovDto> result = new List<RhTmpRetencionesFaovDto>();
           
            if(entities!= null)
            {
                foreach (var item in entities)
                {

                    RhTmpRetencionesFaovDto itemResult = new RhTmpRetencionesFaovDto();

                    itemResult = await MapRhTmpRetencionesFaovDto(item);

                    result.Add(itemResult);
                }
            }
            

           
            return result;



        }



        public async Task<ResultDto<string>> Create (List<RH_H_RETENCIONES_FAOV> entities)
        {

            ResultDto<string> result = new ResultDto<string>("");

            try
            {

                foreach (var item in entities)
                {
                    RH_H_RETENCIONES_FAOV entity = new RH_H_RETENCIONES_FAOV();
                    var nextId = await _repository.GetNextKey();
                    entity.CODIGO_RETENCION_APORTE = nextId;
                    entity.SECUENCIA= item.SECUENCIA;
                    entity.UNIDAD_EJECUTORA = item.UNIDAD_EJECUTORA;
                    entity.CEDULATEXTO = item.CEDULATEXTO;
                    entity.NOMBRES_APELLIDOS = item.NOMBRES_APELLIDOS;
                    entity.DESCRIPCION_CARGO = item.DESCRIPCION_CARGO;
                    entity.FECHA_INGRESO = item.FECHA_INGRESO;
                    entity.MONTO_FAOV_TRABAJADOR = item.MONTO_FAOV_TRABAJADOR;
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
                    entity.REGISTRO_CONCAT = item.REGISTRO_CONCAT;
                    


                    var created = await _repository.Add(entity);
                }

                result.Data = "";
                result.IsValid = true;
                result.Message = "Historico Faov Actualizado";

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

