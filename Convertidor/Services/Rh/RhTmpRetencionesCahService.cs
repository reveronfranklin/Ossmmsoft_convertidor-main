using Convertidor.Services.Sis;
using Ganss.Excel;

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
                    var listRetenciones =  await MapListRhTmpRetencionesCahDto(retenciones);
                    int contador = 0;
                    foreach (var item in listRetenciones)
                    {
                        contador = contador + 1;
                        item.Id = contador;
                    }
                    result.Data = listRetenciones.OrderBy(x=>x.FechaNomina).ToList();;
                }
        
                var deleted =await _repository.Delete(procesoId);
                result.IsValid = true;
                result.Message = "";
                result.LinkData = "";
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

            
                   var data = from s in retenciones
                group s by new
                {
                    
                    CodigoRetencionAporte = s.CODIGO_RETENCION_APORTE,
                    Secuencia = s.SECUENCIA,
                    UnidadEjecutora = s.UNIDAD_EJECUTORA,
                    CedulaTexto = s.CEDULATEXTO,
                    NombresApellidos = s.NOMBRES_APELLIDOS,
                    DescripcionCargo = s.DESCRIPCION_CARGO,
                    FechaIngreso = s.FECHA_INGRESO,
                    MontoCahTrabajador = s.MONTO_CAH_TRABAJADOR,
                    MontoCahPatrono = s.MONTO_CAH_PATRONO,
                    MontoTotalRetencion = s.MONTO_TOTAL_RETENCION,
                    FechaNomina = s.FECHA_NOMINA,
                    SiglasTipoNomina = s.SIGLAS_TIPO_NOMINA,
                    FechaDesde = s.FECHA_DESDE,
                    FechaHasta = s.FECHA_HASTA,
                    CodigoTipoNomina = s.CODIGO_TIPO_NOMINA,
                    
                } into g
                select new RH_H_RETENCIONES_CAH
                {
                    CODIGO_RETENCION_APORTE=g.Key.CodigoRetencionAporte,
                    SECUENCIA=g.Key.Secuencia,
                    UNIDAD_EJECUTORA=g.Key.UnidadEjecutora,
                    CEDULATEXTO=g.Key.CedulaTexto,
                    NOMBRES_APELLIDOS=g.Key.NombresApellidos,
                    DESCRIPCION_CARGO=g.Key.DescripcionCargo,
                    FECHA_INGRESO = g.Key.FechaIngreso,
                    MONTO_CAH_TRABAJADOR=g.Key.MontoCahTrabajador,
                    MONTO_CAH_PATRONO = g.Key.MontoCahPatrono,
                    MONTO_TOTAL_RETENCION=g.Key.MontoTotalRetencion,
                    FECHA_NOMINA=g.Key.FechaNomina,
                    SIGLAS_TIPO_NOMINA=g.Key.SiglasTipoNomina, 
                    FECHA_DESDE=g.Key.FechaDesde,
                    FECHA_HASTA=g.Key.FechaHasta,
                    CODIGO_TIPO_NOMINA=g.Key.CodigoTipoNomina,
                 
                            
                };



                   result = data.ToList();
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
                    MontoCahTrabajador = s.MONTO_CAH_TRABAJADOR,
                    MontoCahPatrono = s.MONTO_CAH_PATRONO,
                    MontoTotalRetencion = s.MONTO_TOTAL_RETENCION,
                    FechaNomina = s.FECHA_NOMINA,
                    SiglasTipoNomina = s.SIGLAS_TIPO_NOMINA,
                    FechaDesde = s.FECHA_DESDE,
                    FechaHasta = s.FECHA_HASTA,
                    CodigoTipoNomina = s.CODIGO_TIPO_NOMINA,
                    
                } into g
                select new RhTmpRetencionesCahDto
                {
                    CodigoRetencionAporte=g.Key.CodigoRetencionAporte,
                    Secuencia=g.Key.Secuencia,
                    UnidadEjecutora=g.Key.UnidadEjecutora,
                    CedulaTexto=g.Key.CedulaTexto,
                    NombresApellidos=g.Key.NombresApellidos,
                    DescripcionCargo=g.Key.DescripcionCargo,
                    FechaIngreso = g.Key.FechaIngreso,
                    MontoCahTrabajador=g.Key.MontoCahTrabajador,
                    MontoCahPatrono = g.Key.MontoCahPatrono,
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

