using Convertidor.Services.Sis;
using Ganss.Excel;

namespace Convertidor.Data.Repository.Rh
{
	public class RhTmpRetencionesSindService : IRhTmpRetencionesSindService
    {
        
   
        private readonly IRhTmpRetencionesSindRepository _repository;
        private readonly IOssConfigServices _ossConfigService;
        private readonly IRhHRetencionesSindService _rrhservice;
        private readonly IConfiguration _configuration;

        public RhTmpRetencionesSindService(IRhTmpRetencionesSindRepository repository,
                                                  IOssConfigServices ossConfigService,
                                                IRhHRetencionesSindService rrhservice,
                                                IConfiguration configuration)
        {
            _repository = repository;
            _ossConfigService = ossConfigService;
            _rrhservice = rrhservice;
            _configuration = configuration;
        }

        public async Task<ResultDto<List<RhTmpRetencionesSindDto>>> GetRetencionesSind(FilterRetencionesDto filter)
        {
            ResultDto<List<RhTmpRetencionesSindDto>> result = new ResultDto<List<RhTmpRetencionesSindDto>>(null);
            try
            {
                var historico = await _rrhservice.GetRetencionesHSind(filter);
                if (historico.Count == 0)
                {
                    int procesoId = 0;
                    procesoId = await _ossConfigService.GetNextByClave("CONSECUTIVO_RETENCIONES");

                    await _repository.Add(procesoId, filter.TipoNomina, filter.FechaDesde, filter.FechaHasta);
                    var retenciones = await _repository.GetByProcesoId(procesoId);
                    if (retenciones.Count > 0)
                    {
                        var listRetenciones = MapRetencionesSindTmpH(retenciones);
                        var created = await _rrhservice.Create(listRetenciones);
                        await _repository.Delete(procesoId);

                    }
                    result.Data = await MapListRhTmpRetencionesSindDto(retenciones);
                }
                else
                {
                    result.Data = historico;

                }
                var linkData= $"";

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
                    var fileName = $"RetencionesSIND desde {desdeFilter} Hasta {hastaFilter} Tipo Nomina {filter.TipoNomina}.xlsx";
                    string newFile = Path.Combine(Directory.GetCurrentDirectory(), ruta, fileName);
                    if (File.Exists(newFile))
                    {
                        File.Delete(newFile);
                    }
                    mapper.Save(newFile, result.Data, $"RetencionesSIND", true);
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

        public List<RH_H_RETENCIONES_SIND> MapRetencionesSindTmpH(List<RH_TMP_RETENCIONES_SIND> retenciones)
        {
            List<RH_H_RETENCIONES_SIND> result = new List<RH_H_RETENCIONES_SIND>();

            foreach (var retencion in retenciones)
            {
                RH_H_RETENCIONES_SIND resultItem = new RH_H_RETENCIONES_SIND();

                resultItem.CODIGO_RETENCION_APORTE = retencion.CODIGO_RETENCION_APORTE;
                resultItem.SECUENCIA = retencion.SECUENCIA;
                resultItem.UNIDAD_EJECUTORA = retencion.UNIDAD_EJECUTORA;
                resultItem.CEDULATEXTO = retencion.CEDULATEXTO;
                resultItem.NOMBRES_APELLIDOS = retencion.NOMBRES_APELLIDOS;
                resultItem.DESCRIPCION_CARGO = retencion.DESCRIPCION_CARGO;
                resultItem.FECHA_INGRESO = retencion.FECHA_INGRESO;
                resultItem.MONTO_SIND_TRABAJADOR = retencion.MONTO_SIND_TRABAJADOR;
                resultItem.MONTO_SIND_PATRONO = retencion.MONTO_SIND_PATRONO;
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

        public async  Task<RhTmpRetencionesSindDto> MapRhTmpRetencionesSindDto(RH_TMP_RETENCIONES_SIND entity)
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

        public async  Task<List<RhTmpRetencionesSindDto>> MapListRhTmpRetencionesSindDto(List<RH_TMP_RETENCIONES_SIND> entities)
        {
            List<RhTmpRetencionesSindDto> result = new List<RhTmpRetencionesSindDto>();

            if (entities !=null) {
                foreach (var item in entities)
                {

                    RhTmpRetencionesSindDto itemResult = new RhTmpRetencionesSindDto();

                    itemResult = await MapRhTmpRetencionesSindDto(item);

                    result.Add(itemResult);
                }
            }
        
            return result;



        }

        
   

          
    }
}

