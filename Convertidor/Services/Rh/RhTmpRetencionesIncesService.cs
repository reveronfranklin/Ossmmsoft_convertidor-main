using System.Globalization;
using Convertidor.Services.Sis;
using Convertidor.Utility;
using Ganss.Excel;

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
               
                int procesoId = 0;
                procesoId = await _ossConfigService.GetNextByClave("CONSECUTIVO_RETENCIONES");
           
                /*DateTime desdeDate;
                DateTime hastaDate;
                string desdeNew = "";
                string hastaNew = "";
                if (DateTime.TryParseExact(filter.FechaDesde, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out desdeDate))
                {
                    // Convertir la fecha al formato deseado
                    desdeNew = desdeDate.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                   
                }
                
                if (DateTime.TryParseExact(filter.FechaHasta, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out hastaDate))
                {
                    // Convertir la fecha al formato deseado
                    hastaNew = hastaDate.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                   
                }*/
                await _repository.Add(procesoId, filter);
                var retenciones = await _repository.GetByProcesoId(procesoId);
                if (retenciones.Count > 0)
                {
                    var contador = 0;
                    var listRetenciones = await MapListRhTmpRetencionesIncesDto(retenciones);
                    foreach (var item in listRetenciones)
                    {
                        contador = contador + 1;
                        item.Id = contador;
                        item.FechaNomina = Fecha.GetFechaString(item.FechaDesde);
                    } 
                    result.Data = listRetenciones.OrderBy(x=>x.FechaNomina).ToList();;

                    await _repository.Delete(procesoId);
            

                }
               
                
                var linkData = $"";
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
                    var fileName = $"RetencionesInce desde {desdeFilter} Hasta {hastaFilter} Tipo Nomina {filter.TipoNomina}.xlsx";
                    string newFile = Path.Combine(Directory.GetCurrentDirectory(), ruta, fileName);
                    if (File.Exists(newFile))
                    {
                        File.Delete(newFile);
                    }

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
                    MontoIncesTrabajador = s.MONTO_INCES_TRABAJADOR,
                    MontoIncesPatrono=s.MONTO_INCES_PATRONO,
                    MontoTotalRetencion=s.MONTO_TOTAL_RETENCION,
                    FechaNomina = s.FECHA_NOMINA,
                    SiglasTipoNomina = s.SIGLAS_TIPO_NOMINA,
                    FechaDesde = s.FECHA_DESDE,
                    FechaHasta = s.FECHA_HASTA,
                    CodigoTipoNomina = s.CODIGO_TIPO_NOMINA,
                    
                } into g
                select new RhTmpRetencionesIncesDto
                {
                    CodigoRetencionAporte=g.Key.CodigoRetencionAporte,
                    Secuencia=g.Key.Secuencia,
                    UnidadEjecutora=g.Key.UnidadEjecutora,
                    CedulaTexto=g.Key.CedulaTexto,
                    NombresApellidos=g.Key.NombresApellidos,
                    DescripcionCargo=g.Key.DescripcionCargo,
                    FechaIngreso = g.Key.FechaIngreso,
                    MontoIncesTrabajador=g.Key.MontoIncesTrabajador,
                    MontoIncesPatrono=g.Key.MontoIncesPatrono,
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