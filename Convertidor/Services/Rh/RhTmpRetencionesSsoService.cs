using System.Globalization;
using Convertidor.Services.Sis;
using Ganss.Excel;

namespace Convertidor.Data.Repository.Rh
{
    public class RhTmpRetencionesSsoService : IRhTmpRetencionesSsoService
    {


        private readonly IRhTmpRetencionesSsoRepository _repository;
        private readonly IOssConfigServices _ossConfigService;
        private readonly IRhHRetencionesSsoService _rhhService;

        private readonly IConfiguration _configuration;


        public RhTmpRetencionesSsoService(IRhTmpRetencionesSsoRepository repository,
                                          IOssConfigServices ossConfigService,
                                          IRhHRetencionesSsoService rhhService,
                                          IConfiguration configuration)
        {
            _repository = repository;
            _ossConfigService = ossConfigService;
            _rhhService = rhhService;
            _configuration = configuration;
        }

        public async Task<ResultDto<List<RhTmpRetencionesSsoDto>>> GetRetencionesSso(FilterRetencionesDto filter)
        {

            ResultDto<List<RhTmpRetencionesSsoDto>> result = new ResultDto<List<RhTmpRetencionesSsoDto>>(null);
            try
            {
                var historico = await _rhhService.GetRetencionesHSso(filter);
                if (historico.Count == 0)
                {
                    int procesoId = 0;
                    procesoId = await _ossConfigService.GetNextByClave("CONSECUTIVO_RETENCIONES");

                    await _repository.Add(procesoId, filter);
                    var retenciones = await _repository.GetByProcesoId(procesoId);
                    if (retenciones != null)
                    {
                        var listRetenciones =await MapListRhTmpRetencionesSsoDto(retenciones);
                        var contador = 0;
                        foreach (var item in listRetenciones)
                        {
                            contador = contador + 1;
                            item.Id = contador;
                        } 
                        result.Data = listRetenciones.OrderBy(x=>x.FechaNomina).ToList();;

                        await _repository.Delete(procesoId);

                    }
                   
                }
                else
                {
                    result.Data = historico;

                }
                var linkData=$"";
                if (result.Data != null && result.Data.Count > 0)
                {
                    ExcelMapper mapper = new ExcelMapper();
                    var settings = _configuration.GetSection("Settings").Get<Settings>();
                    var ruta = @settings.ExcelFiles;  //@"/Users/freveron/Documents/MM/App/full-version/public/ExcelFiles";
                    DateTime desde = DateTime.ParseExact(filter.FechaDesde, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    var mesString = "00" + desde.Month.ToString();
                    var diaString = "00" + desde.Day.ToString();
                    string mes = mesString.Substring(mesString.Length - 2, 2);
                    string dia = diaString.Substring(diaString.Length - 2, 2);
                    var desdeFilter = desde.Year + mes + dia;
                    DateTime hasta = DateTime.ParseExact(filter.FechaHasta, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    var mesHastaString = "00" + hasta.Month.ToString();
                    var diaHastaString = "00" + hasta.Day.ToString();
                    string mesHasta = mesHastaString.Substring(mesHastaString.Length - 2, 2);
                    string diaHasta = diaHastaString.Substring(diaHastaString.Length - 2, 2);
                    var hastaFilter = hasta.Year + mesHasta + diaHasta;
                    var fileName = $"RetencionesSSO desde {desdeFilter} Hasta {hastaFilter} Tipo Nomina {filter.TipoNomina}.xlsx";
                    string newFile = Path.Combine(Directory.GetCurrentDirectory(), ruta, fileName);
                    if (File.Exists(newFile))
                    {
                        File.Delete(newFile);
                    }
                    mapper.Save(newFile, result.Data, $"RetencionesSSO", true);
                    linkData=$"/ExcelFiles/{fileName}";
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

       

        public List<RH_H_RETENCIONES_SSO> MapRetencionesSsoTmpH(List<RH_TMP_RETENCIONES_SSO> retenciones) 
        {
             List<RH_H_RETENCIONES_SSO> result = new List<RH_H_RETENCIONES_SSO>();

            foreach(var retencion in retenciones) 
            {
                RH_H_RETENCIONES_SSO resultItem = new  RH_H_RETENCIONES_SSO();

                resultItem.CODIGO_RETENCION_APORTE = retencion.CODIGO_RETENCION_APORTE;
                resultItem.SECUENCIA = retencion.SECUENCIA;
                resultItem.UNIDAD_EJECUTORA = retencion.UNIDAD_EJECUTORA;
                resultItem.CEDULATEXTO = retencion.CEDULATEXTO;
                resultItem.NOMBRES_APELLIDOS = retencion.NOMBRES_APELLIDOS;
                resultItem.DESCRIPCION_CARGO = retencion.DESCRIPCION_CARGO;
                resultItem.FECHA_INGRESO = retencion.FECHA_INGRESO;
                resultItem.MONTO_SSO_TRABAJADOR = retencion.MONTO_SSO_TRABAJADOR;
                resultItem.MONTO_RPE_TRABAJADOR = retencion.MONTO_RPE_TRABAJADOR;
                resultItem.MONTO_SSO_PATRONO = retencion.MONTO_SSO_PATRONO;
                resultItem.MONTO_RPE_PATRONO = retencion.MONTO_RPE_PATRONO;
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
        public async  Task<RhTmpRetencionesSsoDto> MapRhTmpRetencionesSsoDto(RH_TMP_RETENCIONES_SSO entity)
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

        public async  Task<List<RhTmpRetencionesSsoDto>> MapListRhTmpRetencionesSsoDto(List<RH_TMP_RETENCIONES_SSO> entities)
        {
            List<RhTmpRetencionesSsoDto> result = new List<RhTmpRetencionesSsoDto>();
           
            
              var data = from s in entities
                group s by new
                {
                    
                    CodigoRetencionAporte = s.CODIGO_RETENCION_APORTE,
                    Secuencia = s.SECUENCIA,
                    UnidadEjecutora = s.UNIDAD_EJECUTORA,
                    CedulaTexto = s.CEDULATEXTO,
                    NombresApellidos = s.NOMBRES_APELLIDOS,
                    DescripcionCargo = s.DESCRIPCION_CARGO,
                    FechaIngreso = s.FECHA_INGRESO,
                    MontoSsoTrabajador = s.MONTO_SSO_TRABAJADOR,
                    MontoSsoPatrono=s.MONTO_SSO_PATRONO,
                    MontoTotalRetencion=s.MONTO_TOTAL_RETENCION,
                    FechaNomina = s.FECHA_NOMINA,
                    SiglasTipoNomina = s.SIGLAS_TIPO_NOMINA,
                    FechaDesde = s.FECHA_DESDE,
                    FechaHasta = s.FECHA_HASTA,
                    CodigoTipoNomina = s.CODIGO_TIPO_NOMINA,
                    
                } into g
                select new RhTmpRetencionesSsoDto
                {
                    CodigoRetencionAporte=g.Key.CodigoRetencionAporte,
                    Secuencia=g.Key.Secuencia,
                    UnidadEjecutora=g.Key.UnidadEjecutora,
                    CedulaTexto=g.Key.CedulaTexto,
                    NombresApellidos=g.Key.NombresApellidos,
                    DescripcionCargo=g.Key.DescripcionCargo,
                    FechaIngreso = g.Key.FechaIngreso,
                    MontoSsoTrabajador=g.Key.MontoSsoTrabajador,
                    MontoSsoPatrono=g.Key.MontoSsoPatrono,
                    MontoTotalRetencion=g.Key.MontoTotalRetencion,
                    FechaNomina=g.Key.FechaNomina,
                    SiglasTipoNomina=g.Key.SiglasTipoNomina, 
                    FechaDesde=g.Key.FechaDesde,
                    FechaHasta=g.Key.FechaHasta,
                    CodigoTipoNomina=g.Key.CodigoTipoNomina,
                    
                };
             result = data.ToList();
            
           
            return result;



        }

        
   

          
    }
}

