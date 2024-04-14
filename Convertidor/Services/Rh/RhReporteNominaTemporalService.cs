using System.Globalization;

namespace Convertidor.Data.Repository.Rh
{
	public class RhReporteNominaTemporalService: IRhReporteNominaTemporalService
    {
        
   
        private readonly IRhReporteNominaTemporalRepository _repository;
        private readonly IRhTipoNominaService _rhTipoNominaService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
   

        public RhReporteNominaTemporalService(IRhReporteNominaTemporalRepository repository, 
                                        IRhDescriptivasService descriptivaService, 
                                        IRhTipoNominaService rhTipoNominaService, 
                                        ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _rhTipoNominaService = rhTipoNominaService;
            _sisUsuarioRepository = sisUsuarioRepository;
        }
       
        
        public async Task<ResultDto<List<RhReporteNominaResumenResponseDto>> > GetByPeriodoTipoNominaResumen(FilterRepoteNomina filter)
        {
            try
            {
                ResultDto<List<RhReporteNominaResumenResponseDto>> result = new  ResultDto<List<RhReporteNominaResumenResponseDto>> (null);
                var historico = await _repository.GetByPeriodoTipoNomina(filter.CodigoPeriodo,filter.CodigoTipoNomina);
                if (historico.Count > 0)
                {
                    result.Data = await MapListHistoricoResumen(historico);
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
        public async Task<ResultDto<List<RhReporteNominaResponseDto>> > GetByPeriodoTipoNominaPersona(FilterRepoteNomina filter)
        {
            try
            {
                ResultDto<List<RhReporteNominaResponseDto>> result = new  ResultDto<List<RhReporteNominaResponseDto>> (null);
                var historico = await _repository.GetByPeriodoTipoNominaPersona(filter.CodigoPeriodo,filter.CodigoTipoNomina,(int)filter.CodigoPersona);
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
        
        public async Task<ResultDto<List<RhReporteNominaResumenConceptoResponseDto>> > GetByPeriodoTipoNominaResumenConcepto(FilterRepoteNomina filter)
        {
            try
            {
                ResultDto<List<RhReporteNominaResumenConceptoResponseDto>> result = new  ResultDto<List<RhReporteNominaResumenConceptoResponseDto>> (null);
                var historico = await _repository.GetByPeriodoTipoNomina(filter.CodigoPeriodo,filter.CodigoTipoNomina);
                if (historico.Count > 0)
                {
                    result.Data = await MapListHistoricoResumenConcepto(historico,filter);
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

        public async  Task<List<RhListOficinaDto>> ListIcp(FilterRepoteNomina filter)
        {
            var dtos = await _repository.GetByPeriodoTipoNomina(filter.CodigoPeriodo,filter.CodigoTipoNomina);
            
            var lista = from s in dtos
                group s by new
                {
                    CodigoIcpConcat=s.CODIGO_ICP_CONCAT,
                    CodigoIcp=s.CODIGO_ICP,
                    Denominacion=s.DENOMINACION,
              
                
                } into g
                select new RhListOficinaDto()
                {
              
                    CodigoIcpConcat = g.Key.CodigoIcpConcat,
                    CodigoIcp = g.Key.CodigoIcp,
                    Denominacion = g.Key.Denominacion,
                
                };
                            
                           

            var result = lista.OrderBy(x => x.Denominacion).ToList();
            return result;

        }

        
        public string GetMes(int mes)
        {
            string result;
            int opt = 2;
 
            switch(mes)
            {
                case 1: 
                    result = "Enero";
                    break;
                case 2: 
                    result = "Febrero";
                    break;
                case 3: 
                    result = "Marzo";
                    break;
                case 4: 
                    result = "Abril";
                    break;
                case 5: 
                    result = "Mqyo";
                    break;
                case 6: 
                    result = "Junio";
                    break;
                case 7: 
                    result = "Julio";
                    break;
                case 8: 
                    result = "Agosto";
                    break;
                case 9: 
                    result = "Septiembre";
                    break;
                case 10: 
                    result = "Octubre";
                    break;
                case 11: 
                    result = "Noviembre";
                    break;
                case 12: 
                    result = "Diciembre";
                    break;
                
                default:
                    result = "Error";
                    break;
            }

            return result;
        }
        public async  Task<List<RhReporteNominaResumenConceptoResponseDto>> MapListHistoricoResumenConcepto(List<RH_V_REPORTE_NOMINA_TEMPORAL> dtos,FilterRepoteNomina filter)
        {
            RhTiposNominaFilterDto tiposNominaFilterDto = new RhTiposNominaFilterDto();
            tiposNominaFilterDto.CodigoTipoNomina = filter.CodigoTipoNomina;
            var tipoNomina = await _rhTipoNominaService.GetByCodigo(tiposNominaFilterDto);
               
            
            var lista = from s in dtos
                group s by new { FechaNomina=s.FECHA_NOMINA, NumeroConcepto = s.NUMERO_CONCEPTO ,DenominacionConcepto=s.DENOMINACION_CONCEPTO,Periodo=s.PERIODO,Descripcion=s.DESCRIPCION} into g
                select new RhReporteNominaResumenConceptoResponseDto()
                {
                    FechaNomina=g.Key.FechaNomina,
                    TipoNomina = tipoNomina.Descripcion,
                    NumeroConcepto=g.Key.NumeroConcepto,
                    DenominacionConcepto=g.Key.DenominacionConcepto,
                    Periodo=g.Key.Periodo,
                    Año = g.Key.FechaNomina.Year.ToString(),
                    Mes=GetMes(g.Key.FechaNomina.Month),
                    Descripcion = g.Key.Descripcion,
                    Asignacion = g.Sum(s => s.ASIGNACION),
                    Deduccion = g.Sum(s => s.DEDUCCION),

                };
           
            return lista.ToList();

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
       
    public async  Task<List<RhReporteNominaResumenResponseDto>> MapListHistoricoResumen(List<RH_V_REPORTE_NOMINA_TEMPORAL> dtos)
        {
                            var lista = from s in dtos
                            group s by new
                            {
                                CodigoTipoNomina=s.CODIGO_TIPO_NOMINA,
                                CodigoPeriodo=s.CODIGO_PERIODO,
                                Periodo=s.PERIODO,
                                FechaNomina = s.FECHA_NOMINA,
                                FechaNominaString=s.FECHA_NOMINA.ToString("u"),
                                FechaNominaObj= GetFechaDto(s.FECHA_NOMINA),
                                CodigoIcpConcat=s.CODIGO_ICP_CONCAT,
                                CodigoIcp=s.CODIGO_ICP,
                                Denominacion=s.DENOMINACION,
                                CodigoPersona=s.CODIGO_PERSONA,
                                Nombre=s.NOMBRE,
                                
                            } into g
                            select new RhReporteNominaResumenResponseDto()
                            {
                                CodigoTipoNomina = g.Key.CodigoTipoNomina,
                                CodigoPeriodo = g.Key.CodigoPeriodo,
                                Periodo=g.Key.Periodo,
                                FechaNomina = g.Key.FechaNomina,
                                FechaNominaString = g.Key.FechaNominaString,
                                FechaNominaObj = g.Key.FechaNominaObj,
                                CodigoIcpConcat = g.Key.CodigoIcpConcat,
                                CodigoIcp = g.Key.CodigoIcp,
                                Denominacion = g.Key.Denominacion,
                                CodigoPersona = g.Key.CodigoPersona,
                                Nombre = g.Key.Nombre,
                                
                            };
           
            return lista.ToList();

        }
        public async  Task<List<RhReporteNominaResponseDto>> MapListHistorico(List<RH_V_REPORTE_NOMINA_TEMPORAL> dtos)
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
                                CodigoIcp = g.Key.CodigoIcp,
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

