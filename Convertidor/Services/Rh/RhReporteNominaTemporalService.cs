using System.Globalization;

namespace Convertidor.Data.Repository.Rh
{
	public class RhReporteNominaHistoricoService: IRhReporteNominaHistoricoService
    {
        
   
        private readonly IRhReporteNominaHistoricoRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
   

        public RhReporteNominaHistoricoService(IRhReporteNominaHistoricoRepository repository, 
                                        IRhDescriptivasService descriptivaService, 
                                        ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }
       
        public async Task<ResultDto<List<RhReporteNominaResponseDto>> > GetByPeriodoTipoNomina(FilterRepoteNomina filter)
        {
            try
            {
                ResultDto<List<RhReporteNominaResponseDto>> result = new  ResultDto<List<RhReporteNominaResponseDto>> (null);
                var historico = await _repository.GetByPeriodoTipoNomina(filter.CodigoPeriodo,filter.CodigoTipoNomina);
                if (historico.Count > 0)
                {
                    result.Data = await MapListHistorico(historico);
                    result.Message = "";
                    result.IsValid = true;
                    
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = "No Data";
                    return result;
                }
            

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            FechaDesdeObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            FechaDesdeObj.Month = month.Substring(month.Length - 2);
            FechaDesdeObj.Day = day.Substring(day.Length - 2);
    
            return FechaDesdeObj;
        }
       
  
        public async  Task<List<RhReporteNominaResponseDto>> MapListHistorico(List<RH_V_REPORTE_NOMINA_HISTORICO> dtos)
        {
                            var lista = from s in dtos
                            group s by new
                            {
                                FechaNomina = s.FECHA_NOMINA,
                                FechaNominaString=s.FECHA_NOMINA.ToString("u"),
                                FechaNominaObj= GetFechaDto(s.FECHA_NOMINA),
                                CodigoPeriodo=s.CODIGO_PERIODO,
                                CodigoTipoNomina=s.CODIGO_TIPO_NOMINA,
                                CodigoIcpConcat=s.CODIGO_ICP_CONCAT,
                                CodigoIcp=s.CODIGO_ICP,
                                Denominacion=s.DENOMINACION,
                                DenominacionCargo=s.DENOMINACION_CARGO,
                                Cedula=s.CEDULA,
                                Nombre=s.NOMBRE,
                                NoCuenta=s.NO_CUENTA,
                                NumeroConcepto=s.NUMERO_CONCEPTO,
                                TipoMovConcepto=s.TIPO_MOV_CONCEPTO,
                                DenominacionConcepto=s.DENOMINACION_CONCEPTO,
                                ComplementoConcepto=s.COMPLEMENTO_CONCEPTO,
                                Porcentaje=s.PORCENTAJE,
                                TipoConcepto=s.TIPO_CONCEPTO,
                                Monto=s.MONTO,
                                Asignacion=s.ASIGNACION,
                                Deduccion=s.DEDUCCION,
                                Status=s.STATUS,
                                DescripcionStatus=s.DESCRIPCION_STATUS,
                                CodigoPersona=s.CODIGO_PERSONA,
                                FechaIngreso=s.FECHA_INGRESO,
                                FechaIngresoString=s.FECHA_INGRESO.ToString("u"),
                                FechaIngresoObj=GetFechaDto(s.FECHA_INGRESO),
                                CargoCodigo=s.CARGO_CODIGO,
                                Banco=s.BANCO,
                                CodigoConcepto=s.CODIGO_CONCEPTO,
                                Modulo=s.MODULO,
                                CodigoIdentificador=s.CODIGO_IDENTIFICADOR,
                                CodigoEmpresa=s.CODIGO_EMPRESA,
                                Descripcion=s.DESCRIPCION,
                                
                            } into g
                            select new RhReporteNominaResponseDto()
                            {
                                FechaNomina = g.Key.FechaNomina,
                                FechaNominaString = g.Key.FechaNominaString,
                                FechaNominaObj = g.Key.FechaNominaObj,
                                CodigoPeriodo = g.Key.CodigoPeriodo,
                                CodigoTipoNomina = g.Key.CodigoTipoNomina,
                                CodigoIcpConcat = g.Key.CodigoIcpConcat,
                                Denominacion = g.Key.Denominacion,
                                DenominacionCargo = g.Key.DenominacionCargo,
                                Cedula = g.Key.Cedula,
                                Nombre = g.Key.Nombre,
                                NoCuenta = g.Key.NoCuenta,
                                NumeroConcepto = g.Key.NumeroConcepto,
                                TipoMovConcepto = g.Key.TipoMovConcepto,
                                DenominacionConcepto = g.Key.DenominacionConcepto,
                                ComplementoConcepto = g.Key.ComplementoConcepto,
                                Porcentaje = g.Key.Porcentaje,
                                TipoConcepto = g.Key.TipoConcepto,
                                Monto = g.Key.Monto,
                                Asignacion = g.Key.Asignacion,
                                Deduccion = g.Key.Deduccion,
                                Status = g.Key.Status,
                                DescripcionStatus = g.Key.DescripcionStatus,
                                CodigoPersona = g.Key.CodigoPersona,
                                FechaIngreso = g.Key.FechaIngreso,
                                FechaIngresoString = g.Key.FechaIngresoString,
                                FechaIngresoObj = g.Key.FechaIngresoObj,
                                CargoCodigo = g.Key.CargoCodigo,
                                Banco = g.Key.Banco,
                                CodigoConcepto = g.Key.CodigoConcepto,
                                Modulo = g.Key.Modulo,
                                CodigoIdentificador = g.Key.CodigoIdentificador,
                                CodigoEmpresa = g.Key.CodigoEmpresa,
                                Descripcion = g.Key.Descripcion,
                                
                                
                                
                            };
           
            return lista.ToList();

        }
        
        
        
    }
}

