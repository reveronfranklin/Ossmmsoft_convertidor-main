using Convertidor.Services.Sis;
using Ganss.Excel;

namespace Convertidor.Data.Repository.Rh
{
	public class RhTmpRetencionesFaovService : IRhTmpRetencionesFaovService
    {
        
   
        private readonly IRhTmpRetencionesFaovRepository _repository;
        private readonly IOssConfigServices _ossConfigService;
        private readonly IRhHRetencionesFaovService _rrhservice;
        private readonly IConfiguration _configuration;

        public RhTmpRetencionesFaovService(IRhTmpRetencionesFaovRepository repository, IOssConfigServices ossConfigService,
            IRhHRetencionesFaovService rrhservice,IConfiguration configuration)
        {
            _repository = repository;
            _ossConfigService = ossConfigService;
            _rrhservice = rrhservice;
            _configuration = configuration;
        }
       
        public async Task<ResultDto<List<RhTmpRetencionesFaovDto>>> GetRetencionesFaov(FilterRetencionesDto filter)
        {
            ResultDto<List<RhTmpRetencionesFaovDto>> result = new ResultDto<List<RhTmpRetencionesFaovDto>>(null);
            try
            {   
                if (filter.TipoNomina == 0)
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = "No Data";
                    result.LinkData = "";
                    return result;
                }
                int procesoId = 0;
                procesoId = await _ossConfigService.GetNextByClave("CONSECUTIVO_RETENCIONES");

                await _repository.Add(procesoId, filter.TipoNomina, filter.FechaDesde, filter.FechaHasta);
                var retenciones = await _repository.GetByProcesoId(procesoId);
                if (retenciones != null)
                {
                    var listRetenciones = await MapListRhTmpRetencionesFaovDto(retenciones);
                    var contador = 0;
                    foreach (var item in listRetenciones)
                    {
                        contador = contador + 1;
                        item.Id = contador;
                    }
                    result.Data = listRetenciones.OrderBy(x=>x.FechaNomina).ToList();;
                   
                    await _repository.Delete(procesoId);

                }
                
                var linkData = $"";
                var linkDataArlternative= $"";
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
                var fileName = $"RetencionesFAOV-desde-{desdeFilter}-Hasta-{hastaFilter}-TipoNomina-{filter.TipoNomina}.xlsx";
                var fileNameTxt = $"RetencionesFAOV-desde-{desdeFilter}-Hasta-{hastaFilter}-TipoNomina-{filter.TipoNomina}.txt";
                string newFile = Path.Combine(Directory.GetCurrentDirectory(), ruta, fileName);
                string newFileTxt = Path.Combine(Directory.GetCurrentDirectory(), ruta, fileNameTxt);
                if (File.Exists(newFile))
                {
                    File.Delete(newFile);
                }
                if (File.Exists(newFileTxt))
                {
                    File.Delete(newFileTxt);
                }
                
                mapper.Save(newFile, result.Data, $"RetencionesFAOV", true);
                
                using(TextWriter tw = new StreamWriter(newFileTxt))
                {
                    foreach (var s in result.Data)
                        tw.WriteLine(s.RegistroConcat);
                    tw.Close();
                }
                linkData = $"/ExcelFiles/{fileName}";
                linkDataArlternative= $"/ExcelFiles/{fileNameTxt}";
     
                }
                

                result.IsValid = true;
                result.Message = "";
                result.LinkData = linkData;
                result.LinkDataArlternative= linkDataArlternative;
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

        public List<RH_H_RETENCIONES_FAOV> MapRetencionesFaovTmpH(List<RH_TMP_RETENCIONES_FAOV> retenciones)
        {
            List<RH_H_RETENCIONES_FAOV> result = new List<RH_H_RETENCIONES_FAOV>();

            foreach (var retencion in retenciones)
            {
                RH_H_RETENCIONES_FAOV resultItem = new RH_H_RETENCIONES_FAOV();

                resultItem.CODIGO_RETENCION_APORTE = retencion.CODIGO_RETENCION_APORTE;
                resultItem.SECUENCIA = retencion.SECUENCIA;
                resultItem.UNIDAD_EJECUTORA = retencion.UNIDAD_EJECUTORA;
                resultItem.CEDULATEXTO = retencion.CEDULATEXTO;
                resultItem.NOMBRES_APELLIDOS = retencion.NOMBRES_APELLIDOS;
                resultItem.DESCRIPCION_CARGO = retencion.DESCRIPCION_CARGO;
                resultItem.FECHA_INGRESO = retencion.FECHA_INGRESO;
                resultItem.MONTO_FAOV_TRABAJADOR = retencion.MONTO_FAOV_TRABAJADOR;
                resultItem.MONTO_FAOV_PATRONO = retencion.MONTO_FAOV_PATRONO;
                resultItem.MONTO_TOTAL_RETENCION = retencion.MONTO_TOTAL_RETENCION;
                resultItem.FECHA_NOMINA = retencion.FECHA_NOMINA;
                resultItem.SIGLAS_TIPO_NOMINA = retencion.SIGLAS_TIPO_NOMINA;
                resultItem.REGISTRO_CONCAT = retencion.REGISTRO_CONCAT;
                resultItem.PROCESO_ID = retencion.PROCESO_ID;
                resultItem.FECHA_DESDE = retencion.FECHA_DESDE;
                resultItem.FECHA_HASTA = retencion.FECHA_HASTA;
                resultItem.CODIGO_TIPO_NOMINA = retencion.CODIGO_TIPO_NOMINA;
                resultItem.REGISTRO_CONCAT = retencion.REGISTRO_CONCAT;

                result.Add(resultItem);



            }

            return result;

        }

        public async  Task<RhTmpRetencionesFaovDto> MapRhTmpRetencionesFaovDto(RH_TMP_RETENCIONES_FAOV entity)
        {


                RhTmpRetencionesFaovDto itemResult = new RhTmpRetencionesFaovDto();
                itemResult.CodigoRetencionAporte = entity.CODIGO_RETENCION_APORTE;
                itemResult.Secuencia = entity.SECUENCIA;
                itemResult.UnidadEjecutora = entity.UNIDAD_EJECUTORA;
                itemResult.CedulaTexto = entity.CEDULATEXTO;
                itemResult.NombresApellidos = entity.NOMBRES_APELLIDOS;
                itemResult.DescripcionCargo= entity.DESCRIPCION_CARGO;
                itemResult.FechaIngreso = entity.FECHA_INGRESO;
                itemResult.MontoFaovTrabajador = entity.MONTO_FAOV_TRABAJADOR;
                itemResult.MontoFaovPatrono = entity.MONTO_FAOV_PATRONO;
                itemResult.MontoTotalRetencion = entity.MONTO_TOTAL_RETENCION;
                itemResult.FechaNomina = entity.FECHA_NOMINA;
                itemResult.RegistroConcat = entity.REGISTRO_CONCAT;
                itemResult.SiglasTipoNomina = entity.SIGLAS_TIPO_NOMINA;
                itemResult.FechaDesde = entity.FECHA_DESDE;
                itemResult.FechaHasta = entity.FECHA_HASTA;
                itemResult.CodigoTipoNomina = entity.CODIGO_TIPO_NOMINA;
                itemResult.RegistroConcat = entity.REGISTRO_CONCAT;

            return itemResult;

        }

        public async  Task<List<RhTmpRetencionesFaovDto>> MapListRhTmpRetencionesFaovDto(List<RH_TMP_RETENCIONES_FAOV> entities)
        {
            List<RhTmpRetencionesFaovDto> result = new List<RhTmpRetencionesFaovDto>();
            
            

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
                    FechaEgreso=s.FECHA_EGRESO,
                    MontoFaovTrabajador = s.MONTO_FAOV_TRABAJADOR,
                    MontoFaovPatrono=s.MONTO_FAOV_PATRONO,
                    MontoTotalRetencion=s.MONTO_TOTAL_RETENCION,
                    FechaNomina = s.FECHA_NOMINA,
                    SiglasTipoNomina = s.SIGLAS_TIPO_NOMINA,
                    RegistroConcat=s.REGISTRO_CONCAT,
                    FechaDesde = s.FECHA_DESDE,
                    FechaHasta = s.FECHA_HASTA,
                    CodigoTipoNomina = s.CODIGO_TIPO_NOMINA,
                    
                } into g
                select new RhTmpRetencionesFaovDto
                {
                    CodigoRetencionAporte=g.Key.CodigoRetencionAporte,
                    Secuencia=g.Key.Secuencia,
                    UnidadEjecutora=g.Key.UnidadEjecutora,
                    CedulaTexto=g.Key.CedulaTexto,
                    NombresApellidos=g.Key.NombresApellidos,
                    DescripcionCargo=g.Key.DescripcionCargo,
                    FechaIngreso = g.Key.FechaIngreso,
                    FechaEgreso = g.Key.FechaEgreso,
                    MontoFaovTrabajador=g.Key.MontoFaovTrabajador,
                    MontoFaovPatrono=g.Key.MontoFaovPatrono,
                    MontoTotalRetencion=g.Key.MontoTotalRetencion,
                    FechaNomina=g.Key.FechaNomina,
                    SiglasTipoNomina=g.Key.SiglasTipoNomina, 
                    RegistroConcat=g.Key.RegistroConcat,
                    FechaDesde=g.Key.FechaDesde,
                    FechaHasta=g.Key.FechaHasta,
                    CodigoTipoNomina=g.Key.CodigoTipoNomina,
                 
                            
                };


                result = data.ToList();
            return result;

        }

    }
}

