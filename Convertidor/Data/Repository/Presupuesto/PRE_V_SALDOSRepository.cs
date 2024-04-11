using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class PRE_V_SALDOSRepository: IPRE_V_SALDOSRepository
    {
		

        private readonly DataContextPre _context;

        public PRE_V_SALDOSRepository(DataContextPre context)
        {
            _context = context;
        }


        public async Task<List<PreFinanciadoDto>> GetListFinanciadoPorPresupuesto(int codigoPresupuesto)
        {
            List<PreFinanciadoDto> result = new List<PreFinanciadoDto>();

            var preVSaldos = await _context.PRE_V_SALDOS.Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto ).ToListAsync();

            if (preVSaldos.Count > 0)
            {

          

                var resumen = from s in preVSaldos
                              group s by new { CodigoPresupuesto = s.CODIGO_PRESUPUESTO , FinanciadoId =s.FINANCIADO_ID,DescripcionFinanciado=s.DESCRIPTIVA_FINANCIADO } into g
                              select new
                              {
                                  g.Key.CodigoPresupuesto,
                                  g.Key.DescripcionFinanciado,
                                  g.Key.FinanciadoId,
                               

                              };
                if (resumen.Count() > 0)
                {
                    foreach (var item in resumen)
                    {
                        PreFinanciadoDto resultItem = new PreFinanciadoDto();
                        resultItem.FinanciadoId = (int)item.FinanciadoId;

                        
                        resultItem.DescripcionFinanciado = item.DescripcionFinanciado;
                        result.Add(resultItem);

                    }

                }

            }


            return result.OrderBy(x=>x.FinanciadoId).ToList();
        }



        public async Task RecalcularSaldo(int codigo_presupuesto)
        {

            var presupuestoActual = await _context.PRE_V_SALDOS.DefaultIfEmpty().OrderByDescending(x => x.CODIGO_PRESUPUESTO).FirstOrDefaultAsync();
            if (presupuestoActual!=null)
            {
                if (codigo_presupuesto != (int)presupuestoActual.CODIGO_PRESUPUESTO)
                {
                    return;
                }
            }
           


            var parameters = new OracleParameter[]
            {
                    new OracleParameter("Codigo_Presupuesto", codigo_presupuesto)
            };

            try
            {


                FormattableString xquery = $"DECLARE \nBEGIN\nPRE.PRE_ACTUALIZAR_SALDOS({codigo_presupuesto});\nEND;";
                        var result = _context.Database.ExecuteSqlInterpolated(xquery);

                FormattableString xqueryDiario = $"DECLARE \nBEGIN\nPRE.PRE_CREATE_SALDOS_DIARIOS({DateTime.Now},{codigo_presupuesto});\nEND;";

                var resultDiario =  _context.Database.ExecuteSqlInterpolated(xqueryDiario);

                var aprobacion = result; 

            }
            catch (Exception ex)
            {
                var mess = ex.InnerException.Message;

                throw;
            }




        }

        public async Task<IEnumerable<PRE_V_SALDOS>> GetAll(FilterPRE_V_SALDOSDto filter)
        {
            try
            {

                var result = await _context.PRE_V_SALDOS.DefaultIfEmpty()
                    .Where(x=>x.CODIGO_EMPRESA==filter.CodigoEmpresa && x.ANO>=filter.AnoDesde && x.ANO<= filter.AnoHasta).ToListAsync();
                return (IEnumerable<PRE_V_SALDOS>)result;
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public async Task<List<PRE_V_SALDOS>> GetAllByPresupuesto(int codigoPresupuesto)
        {
            try
            {

                var result = await _context.PRE_V_SALDOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PRESUPUESTO== codigoPresupuesto).ToListAsync();
                return (List<PRE_V_SALDOS>)result;
            }
            catch (Exception ex)
            {

                return null;
            }

        }
        
        public async Task<bool> PresupuestoExiste(int codigoPresupuesto)
        {
            try
            {
                bool result = false;
                var saldo = await _context.PRE_V_SALDOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PRESUPUESTO== codigoPresupuesto).FirstOrDefaultAsync();
                if (saldo != null)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {

                return false;
            }

        }


        public async Task<List<PRE_V_SALDOS>> GetAllByPresupuestoPucConcat(FilterPresupuestoPucConcat filter)
        {
            try
            {


                List<PRE_V_SALDOS> result = new List<PRE_V_SALDOS>();

                

                if(filter.CodigoPresupuesto == 0 && filter.CodigoPucConcat.Length == 0)
                {

                    var last = await _context.PRE_PRESUPUESTOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PRESUPUESTO)
                    .FirstOrDefaultAsync();
                    result = await _context.PRE_V_SALDOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PRESUPUESTO == last.CODIGO_PRESUPUESTO).ToListAsync();


                }
                if (filter.CodigoPresupuesto>0 && filter.CodigoPucConcat.Length>0)
                {
                    result = await _context.PRE_V_SALDOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto && x.CODIGO_PUC_CONCAT == filter.CodigoPucConcat).ToListAsync();
                }
                if (filter.CodigoPresupuesto > 0 && filter.CodigoPucConcat.Length == 0)
                {
                    result = await _context.PRE_V_SALDOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto ).ToListAsync();
                }




                return (List<PRE_V_SALDOS>)result;
            }
            catch (Exception ex)
            {

                return null;
            }

        }
   

        public async Task<ResultDto<List<PreDenominacionPorPartidaDto?>>> GetPreVDenominacionPorPartidaPuc(FilterPreDenominacionDto filter)
        {
            ResultDto<List<PreDenominacionPorPartidaDto?>> result = new ResultDto<List<PreDenominacionPorPartidaDto?>>(null);
            List<PreDenominacionPorPartidaDto> resultDetail = new List<PreDenominacionPorPartidaDto>();


            try
            {

                var planVSaldos = await _context.PRE_V_SALDOS
                 .Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto).ToListAsync();

                var planUnicoCuenta = await _context.PRE_PLAN_UNICO_CUENTAS
               .Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto ).ToListAsync();



                if (filter.Nivel>=1)
                {
                    var planUnicoCuentaPorPartidaNivelUno = planUnicoCuenta
                    .Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto &&
                    x.CODIGO_GRUPO == filter.CodigoGrupo &&
                    x.CODIGO_NIVEL1 != "00" &&
                    x.CODIGO_NIVEL2 == "00" &&
                    x.CODIGO_NIVEL3 == "00" &&
                    x.CODIGO_NIVEL4 == "00" && x.CODIGO_NIVEL5 == "00").ToList();

                    if (planUnicoCuentaPorPartidaNivelUno.Count() > 0)
                    {
                        foreach (var item in planUnicoCuentaPorPartidaNivelUno)
                        {
                            PreDenominacionPorPartidaDto resultDetailItem = new PreDenominacionPorPartidaDto();
                            resultDetailItem.Nivel = 1;
                            resultDetailItem.CodigoPresupuesto = (int)item.CODIGO_PRESUPUESTO;

                            resultDetailItem.CodigoPartida = item.CODIGO_GRUPO + item.CODIGO_NIVEL1;
                            resultDetailItem.CodigoGenerica = item.CODIGO_NIVEL2;
                            resultDetailItem.CodigoEspecifica = item.CODIGO_NIVEL3;
                            resultDetailItem.CodigoSubEspecifica = item.CODIGO_NIVEL4;
                            resultDetailItem.CodigoNivel5 = item.CODIGO_NIVEL5;
                            resultDetailItem.CodigoPucConcat = item.CODIGO_GRUPO + "." + item.CODIGO_NIVEL1 + "." + item.CODIGO_NIVEL2 + "." + item.CODIGO_NIVEL3 + "." + item.CODIGO_NIVEL4 + "." + item.CODIGO_NIVEL5;
                            resultDetailItem.DenominacionPuc = item.DENOMINACION;


                            resultDetailItem.Presupuestado = await GetPresupuestadoNivelUno(planVSaldos, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1,filter);
                            resultDetailItem.Modificado = await GetModificadoNivelUno(planVSaldos, filter.FinanciadoId, filter.FechaHasta, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1);
                            resultDetailItem.Comprometido = await GetComprometidoNivelUno(planVSaldos, filter.FinanciadoId, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1);
                            resultDetailItem.Bloqueado = await GetBloqueadoNivelUno(planVSaldos, filter.FinanciadoId, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1);
                            resultDetailItem.Causado = await GetCausadoNivelUno(planVSaldos, filter.FinanciadoId, filter.FechaHasta, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1);
                            resultDetailItem.Pagado = await GetPagadoNivelUno(planVSaldos, filter.FinanciadoId, filter.FechaHasta, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1);
                            resultDetailItem.Deuda = resultDetailItem.Comprometido - resultDetailItem.Pagado;
                            resultDetailItem.Disponibilidad = (resultDetailItem.Presupuestado + resultDetailItem.Modificado) - resultDetailItem.Comprometido;
                            resultDetailItem.Asignacion = await GetAsignacionNivelUno(planVSaldos, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1);
                            resultDetailItem.DisponibilidadFinan = GetDisponibilidadFinanciera(resultDetailItem.Asignacion, resultDetailItem.Comprometido, resultDetailItem.Modificado);
                            resultDetail.Add(resultDetailItem);

                        }

                    }

                }
                if (filter.Nivel >= 2)
                {
                    var planUnicoCuentaPorPartidaNivelDos = planUnicoCuenta
                    .Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto &&
                                x.CODIGO_GRUPO == filter.CodigoGrupo &&
                                x.CODIGO_NIVEL1 != "00" &&
                                x.CODIGO_NIVEL2 != "00" &&
                                x.CODIGO_NIVEL3 == "00" &&
                                x.CODIGO_NIVEL4 == "00" &&
                                x.CODIGO_NIVEL5 == "00").ToList();

                    if (planUnicoCuentaPorPartidaNivelDos.Count > 0)
                    {
                        foreach (var item in planUnicoCuentaPorPartidaNivelDos)
                        {
                            PreDenominacionPorPartidaDto resultDetailItem = new PreDenominacionPorPartidaDto();
                            resultDetailItem.Nivel = 2;
                            resultDetailItem.CodigoPresupuesto = (int)item.CODIGO_PRESUPUESTO;

                            resultDetailItem.CodigoPartida = item.CODIGO_GRUPO + item.CODIGO_NIVEL1;
                            resultDetailItem.CodigoGenerica = item.CODIGO_NIVEL2;
                            resultDetailItem.CodigoEspecifica = item.CODIGO_NIVEL3;
                            resultDetailItem.CodigoSubEspecifica = item.CODIGO_NIVEL4;
                            resultDetailItem.CodigoNivel5 = item.CODIGO_NIVEL5;
                            resultDetailItem.CodigoPucConcat = item.CODIGO_GRUPO + "." + item.CODIGO_NIVEL1 + "." + item.CODIGO_NIVEL2 + "." + item.CODIGO_NIVEL3 + "." + item.CODIGO_NIVEL4 + "." + item.CODIGO_NIVEL5;
                            resultDetailItem.DenominacionPuc = item.DENOMINACION;


                            resultDetailItem.Presupuestado = await GetPresupuestadoNivelDos(planVSaldos, resultDetailItem.CodigoPresupuesto,filter.FinanciadoId, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2);
                            resultDetailItem.Modificado = await GetModificadoNivelDos(planVSaldos, filter.FinanciadoId, filter.FechaHasta, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2);

                            resultDetailItem.Comprometido = await GetComprometidoNivelDos(planVSaldos, filter.FinanciadoId, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2);
                            resultDetailItem.Bloqueado = await GetBloqueadoNivelDos(planVSaldos, filter.FinanciadoId, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2);
                            resultDetailItem.Causado = await GetCausadoNivelDos(planVSaldos, filter.FinanciadoId, filter.FechaHasta, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2);
                            resultDetailItem.Pagado = await GetPagadoNivelDos(planVSaldos, filter.FinanciadoId, filter.FechaHasta, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2);
                            resultDetailItem.Deuda = resultDetailItem.Comprometido - resultDetailItem.Pagado;
                            resultDetailItem.Disponibilidad = resultDetailItem.Presupuestado + resultDetailItem.Modificado - resultDetailItem.Comprometido;
                            resultDetailItem.Asignacion = await GetAsignacionNivelDos(planVSaldos, resultDetailItem.CodigoPresupuesto, filter.FinanciadoId, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2);
                            resultDetailItem.DisponibilidadFinan = GetDisponibilidadFinanciera(resultDetailItem.Asignacion, resultDetailItem.Comprometido, resultDetailItem.Modificado);
                            resultDetail.Add(resultDetailItem);

                        }

                    }
                }

                if (filter.Nivel >= 3)
                {
                    var planUnicoCuentaPorPartidaNivelTres = planUnicoCuenta
                     .Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto &&
                                 x.CODIGO_GRUPO == filter.CodigoGrupo &&
                                 x.CODIGO_NIVEL1 != "00" &&
                                 x.CODIGO_NIVEL2 != "00" &&
                                 x.CODIGO_NIVEL3 != "00" &&
                                 x.CODIGO_NIVEL4 == "00" &&
                                 x.CODIGO_NIVEL5 == "00").ToList();

                    if (planUnicoCuentaPorPartidaNivelTres.Count > 0)
                    {
                        foreach (var item in planUnicoCuentaPorPartidaNivelTres)
                        {
                            PreDenominacionPorPartidaDto resultDetailItem = new PreDenominacionPorPartidaDto();
                            resultDetailItem.Nivel = 3;
                            resultDetailItem.CodigoPresupuesto = (int)item.CODIGO_PRESUPUESTO;

                            resultDetailItem.CodigoPartida = item.CODIGO_GRUPO + item.CODIGO_NIVEL1;
                            resultDetailItem.CodigoGenerica = item.CODIGO_NIVEL2;
                            resultDetailItem.CodigoEspecifica = item.CODIGO_NIVEL3;
                            resultDetailItem.CodigoSubEspecifica = item.CODIGO_NIVEL4;
                            resultDetailItem.CodigoNivel5 = item.CODIGO_NIVEL5;
                            resultDetailItem.CodigoPucConcat = item.CODIGO_GRUPO + "." + item.CODIGO_NIVEL1 + "." + item.CODIGO_NIVEL2 + "." + item.CODIGO_NIVEL3 + "." + item.CODIGO_NIVEL4 + "." + item.CODIGO_NIVEL5;
                            resultDetailItem.DenominacionPuc = item.DENOMINACION;


                            resultDetailItem.Presupuestado = await GetPresupuestadoNivelTres(planVSaldos, resultDetailItem.CodigoPresupuesto,filter.FinanciadoId, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2, item.CODIGO_NIVEL3);
                            resultDetailItem.Modificado = await GetModificadoNivelTres(planVSaldos, filter.FinanciadoId, filter.FechaDesde, filter.FechaHasta, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2, item.CODIGO_NIVEL3);

                            resultDetailItem.Comprometido = await GetComprometidoNivelTres(planVSaldos, filter.FinanciadoId, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2, item.CODIGO_NIVEL3);
                            resultDetailItem.Bloqueado = await GetBloqueadoNivelTres(planVSaldos, filter.FinanciadoId, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2, item.CODIGO_NIVEL3);
                            resultDetailItem.Causado = await GetCausadoNivelTres(planVSaldos, filter.FinanciadoId, filter.FechaDesde, filter.FechaHasta, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2, item.CODIGO_NIVEL3);
                            resultDetailItem.Pagado = await GetPagadoNivelTres(planVSaldos, filter.FinanciadoId, filter.FechaDesde, filter.FechaHasta, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2, item.CODIGO_NIVEL3);
                            resultDetailItem.Deuda = resultDetailItem.Comprometido - resultDetailItem.Pagado;
                            resultDetailItem.Disponibilidad = resultDetailItem.Presupuestado + resultDetailItem.Modificado - resultDetailItem.Comprometido;
                            resultDetailItem.Asignacion = await GetAsignacionNivelTres(planVSaldos, resultDetailItem.CodigoPresupuesto, filter.FinanciadoId, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2, item.CODIGO_NIVEL3);



                            resultDetailItem.DisponibilidadFinan = GetDisponibilidadFinanciera(resultDetailItem.Asignacion, resultDetailItem.Comprometido, resultDetailItem.Modificado);
                            resultDetail.Add(resultDetailItem);

                        }

                    }
                }

                if (filter.Nivel >= 4)
                {
                    var planUnicoCuentaPorPartidaNivelCuatro = planUnicoCuenta
                .Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto &&
                            x.CODIGO_GRUPO == filter.CodigoGrupo &&
                            x.CODIGO_NIVEL1 != "00" &&
                            x.CODIGO_NIVEL2 != "00" &&
                            x.CODIGO_NIVEL3 != "00" &&
                            x.CODIGO_NIVEL4 != "00" &&
                            x.CODIGO_NIVEL5 == "00").ToList();

                    if (planUnicoCuentaPorPartidaNivelCuatro.Count > 0)
                    {
                        foreach (var item in planUnicoCuentaPorPartidaNivelCuatro)
                        {
                            PreDenominacionPorPartidaDto resultDetailItem = new PreDenominacionPorPartidaDto();
                            resultDetailItem.Nivel = 4;
                            resultDetailItem.CodigoPresupuesto = (int)item.CODIGO_PRESUPUESTO;

                            resultDetailItem.CodigoPartida = item.CODIGO_GRUPO + item.CODIGO_NIVEL1;
                            resultDetailItem.CodigoGenerica = item.CODIGO_NIVEL2;
                            resultDetailItem.CodigoEspecifica = item.CODIGO_NIVEL3;
                            resultDetailItem.CodigoSubEspecifica = item.CODIGO_NIVEL4;
                            resultDetailItem.CodigoNivel5 = item.CODIGO_NIVEL5;
                            resultDetailItem.CodigoPucConcat = item.CODIGO_GRUPO + "." + item.CODIGO_NIVEL1 + "." + item.CODIGO_NIVEL2 + "." + item.CODIGO_NIVEL3 + "." + item.CODIGO_NIVEL4 + "." + item.CODIGO_NIVEL5;
                            resultDetailItem.DenominacionPuc = item.DENOMINACION;


                            resultDetailItem.Presupuestado = await GetPresupuestadoNivelCuatro(planVSaldos, resultDetailItem.CodigoPresupuesto, filter.FinanciadoId, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2, item.CODIGO_NIVEL3, item.CODIGO_NIVEL4);
                            resultDetailItem.Modificado = await GetModificadoNivelCuatro(planVSaldos, filter.FinanciadoId, filter.FechaHasta, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2, item.CODIGO_NIVEL3, item.CODIGO_NIVEL4);

                            resultDetailItem.Comprometido = await GetComprometidoNivelCuatro(planVSaldos, filter.FinanciadoId, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2, item.CODIGO_NIVEL3, item.CODIGO_NIVEL4);
                            resultDetailItem.Bloqueado = await GetBloqueadoNivelCuatro(planVSaldos, filter.FinanciadoId, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2, item.CODIGO_NIVEL3, item.CODIGO_NIVEL4);
                            resultDetailItem.Causado = await GetCausadoNivelCuatro(planVSaldos, filter.FinanciadoId, filter.FechaHasta, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2, item.CODIGO_NIVEL3, item.CODIGO_NIVEL4);
                            resultDetailItem.Pagado = await GetPagadoNivelCuatro(planVSaldos, filter.FinanciadoId, filter.FechaHasta, resultDetailItem.CodigoPresupuesto, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2, item.CODIGO_NIVEL3, item.CODIGO_NIVEL4);
                            resultDetailItem.Deuda = resultDetailItem.Comprometido - resultDetailItem.Pagado;
                            resultDetailItem.Disponibilidad = resultDetailItem.Presupuestado + resultDetailItem.Modificado - resultDetailItem.Comprometido;
                            resultDetailItem.Asignacion = await GetAsignacionNivelCuatro(planVSaldos, resultDetailItem.CodigoPresupuesto, filter.FinanciadoId, item.CODIGO_GRUPO, item.CODIGO_NIVEL1, item.CODIGO_NIVEL2, item.CODIGO_NIVEL3, item.CODIGO_NIVEL4);
                            resultDetailItem.DisponibilidadFinan = GetDisponibilidadFinanciera(resultDetailItem.Asignacion, resultDetailItem.Comprometido, resultDetailItem.Modificado);
                            resultDetail.Add(resultDetailItem);

                        }

                    }

                }

               
               

                var sortData = resultDetail.OrderBy(x => x.CodigoPartida)
                                            .ThenBy(x => x.CodigoPartida)
                                            .ThenBy(x => x.CodigoGenerica)
                                            .ThenBy(x => x.CodigoEspecifica)
                                            .ThenBy(x => x.CodigoSubEspecifica)
                                            .ThenBy(x => x.CodigoNivel5).ToList();
                if (resultDetail.Count > 0)
                {

                    result.IsValid = true;
                    result.Data = sortData;
                    result.Message = "";

                }
                else
                {
                    result.IsValid = true;
                    result.Data = null;
                    result.Message = "No data";

                }

                return result;
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Data = null;
                result.Message = ex.Message;
                return result;
            }

          
        }
        public decimal GetDisponibilidadFinanciera(decimal asignacion, decimal comprometido, decimal modificado)
        {
            //SUM(case when(NVL(AA.ASIGNACION - NVL(BB.COMPROMETIDO, 0) + NVL(CC.MODIFICADO, 0), 0)) > 0 then(NVL(AA.ASIGNACION - NVL(BB.COMPROMETIDO, 0) + NVL(CC.MODIFICADO, 0), 0)) else 0 end) DISPONIBILIDAD_FINAN

            decimal result;
            result = 0;

            var calculo = (asignacion -comprometido ) + modificado;
            if (calculo > 0)
            {
                result = calculo;
            }
            else
            {
                result = 0;
            }

            return result;
        }


        #region NivelUno

            public async Task<decimal> GetPresupuestadoNivelUno(List<PRE_V_SALDOS> saldos,int codigoPresupuesto, string codigoGrupo, string codigoPartida, FilterPreDenominacionDto filter)
            {
                decimal result = 0;


            //filter
                List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();

                if (filter.FinanciadoId > 0) {
                    preVSaldos = saldos.Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto && x.FINANCIADO_ID==filter.FinanciadoId && x.CODIGO_GRUPO == codigoGrupo && x.CODIGO_PARTIDA == codigoPartida).ToList();
                }
                else {
                    preVSaldos = saldos.Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto && x.CODIGO_GRUPO == codigoGrupo && x.CODIGO_PARTIDA == codigoPartida).ToList();
                }
               

                if (preVSaldos.Count > 0)
                {
                   
                    var resumen = from s in preVSaldos.Where(x=>x.ASIGNACION>=0)
                                  group s by new { CodigoPresupuesto = s.CODIGO_PRESUPUESTO } into g
                                  select new
                                  {
                                      g.Key.CodigoPresupuesto,

                                      PRESUPUESTADO = g.Sum(s => s.PRESUPUESTADO),

                                  };
                    if (resumen.Count() > 0)
                    {
                        var itemResumen = resumen.FirstOrDefault();
                        if (itemResumen != null)
                        {
                            result = itemResumen.PRESUPUESTADO;
                        }


                    }

            }
         

            return result;
            }

        public async Task<decimal> GetModificadoNivelUno(List<PRE_V_SALDOS> saldos,int finaciadoId, DateTime fechaHasta, int codigoPresupuesto, string codigoGrupo, string codigoPartida)
            {

                decimal result = 0;
                List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
                if (finaciadoId > 0) {
                    preVSaldos = saldos
                           .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                       x.FINANCIADO_ID == finaciadoId &&
                                       x.CODIGO_GRUPO == codigoGrupo &&
                                       x.CODIGO_PARTIDA == codigoPartida).ToList();
                }
                else {
                  preVSaldos = saldos
                          .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                  
                                      x.CODIGO_GRUPO == codigoGrupo &&
                                      x.CODIGO_PARTIDA == codigoPartida).ToList();
                }
           

                if (preVSaldos.Count > 0)
                {
                    foreach (var item in preVSaldos)
                    {
                    // SUM(nvl(CASE WHEN ( :P_FINANCIADO_ID = 92 OR NVL(:P_FINANCIADO_ID,0) = 0) THEN DECODE(A.FINANCIADO_ID,719,0,C.MODIFICADO) ELSE C.MODIFICADO END,0)) MODIFICADO,
                    List<PRE_SALDOS_DIARIOS> preVSaldosDiarios = new List<PRE_SALDOS_DIARIOS>();
                   
                        preVSaldosDiarios = await _context.PRE_SALDOS_DIARIOS.DefaultIfEmpty()
                                                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                                                x.CODIGO_SALDO == item.CODIGO_SALDO &&
                                                                x.FECHA_SALDO <= fechaHasta).ToListAsync();


                   


                    if (preVSaldosDiarios.Count > 0)
                        {
                        /*foreach (var itemDiario in preVSaldosDiarios)
                        {
                            if (finaciadoId == 92 || finaciadoId == 0)
                            {
                                if (item.FINANCIADO_ID == 719)
                                {
                                    result = result + 0;
                                }
                                else
                                {
                                    result = result + itemDiario.MODIFICADO;
                                }

                            }
                            else
                            {
                                result = result + itemDiario.MODIFICADO;
                            }
                        }*/
                        if (finaciadoId != 92 && finaciadoId != 0)
                        {
                            var resumen = from s in preVSaldos
                                          group s by new { CodigoPresupuesto = s.CODIGO_PRESUPUESTO } into g
                                          select new
                                          {
                                              g.Key.CodigoPresupuesto,

                                              MODIFICADO = g.Sum(s => s.MODIFICADO),

                                          };
                            if (resumen.Count() > 0)
                            {
                                var itemResumen = resumen.FirstOrDefault();
                                if (itemResumen != null)
                                {
                                    result = itemResumen.MODIFICADO;
                                }


                            }

                        }
                        else
                        {
                            var resumen = from s in preVSaldos
                                          where s.FINANCIADO_ID!=719
                                          group s by new { CodigoPresupuesto = s.CODIGO_PRESUPUESTO } into g
                                          select new
                                          {
                                              g.Key.CodigoPresupuesto,

                                              MODIFICADO = g.Sum(s => s.MODIFICADO),

                                          };
                            if (resumen.Count() > 0)
                            {
                                var itemResumen = resumen.FirstOrDefault();
                                if (itemResumen != null)
                                {
                                    result = itemResumen.MODIFICADO;
                                }


                            }
                        }
                           


                    }

                    }
                }

                return result;
            }
            public async Task<decimal> GetComprometidoNivelUno(List<PRE_V_SALDOS> saldos, int finaciadoId, int codigoPresupuesto, string codigoGrupo, string codigoPartida)
            {

                decimal result = 0;
                List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
                if (finaciadoId > 0) {
                    preVSaldos = saldos
                           .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                       x.FINANCIADO_ID == finaciadoId &&
                                       x.CODIGO_GRUPO == codigoGrupo &&
                                       x.CODIGO_PARTIDA == codigoPartida).ToList();
                }
                else {
                    preVSaldos = saldos
                          .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                     
                                      x.CODIGO_GRUPO == codigoGrupo &&
                                      x.CODIGO_PARTIDA == codigoPartida).ToList();
                 }
           

                if (preVSaldos.Count > 0)
                {
                    foreach (var item in preVSaldos)
                    {
                        //   SUM(nvl(CASE WHEN (:P_FINANCIADO_ID = 92 OR NVL(:P_FINANCIADO_ID,0) = 0) THEN DECODE(A.FINANCIADO_ID,719,0,C.COMPROMETIDO) ELSE C.COMPROMETIDO END,0)) COMPROMETIDO,
                        if (finaciadoId == 92 || finaciadoId == 0)
                        {
                            if (item.FINANCIADO_ID != 719)
                            {
                              
                                result = result + item.COMPROMETIDO;
                            }

                        }
                        else
                        {
                            result = result + item.COMPROMETIDO;
                        }
                    }
                }

                return result;
            }
            public async Task<decimal> GetBloqueadoNivelUno(List<PRE_V_SALDOS> saldos, int finaciadoId, int codigoPresupuesto, string codigoGrupo, string codigoPartida)
            {

                decimal result = 0;
                List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
                if (finaciadoId > 0) {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               x.FINANCIADO_ID == finaciadoId &&
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida).ToList();
                }
                else {

                    preVSaldos = saldos
                               .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                         
                                           x.CODIGO_GRUPO == codigoGrupo &&
                                           x.CODIGO_PARTIDA == codigoPartida).ToList();
                }
               

                if (preVSaldos.Count > 0)
                {
                    foreach (var item in preVSaldos)
                    {
                        result = result + item.BLOQUEADO;
                    }
                }

                return result;
            }
            public async Task<decimal> GetCausadoNivelUno(List<PRE_V_SALDOS> saldos, int financiadoId, DateTime fechaHasta, int codigoPresupuesto, string codigoGrupo, string codigoPartida)
        {

            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               x.FINANCIADO_ID == financiadoId &&
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida).ToList();
            }
            else {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                              
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida).ToList();
            }
           

            if (preVSaldos.Count > 0)
            {
                foreach (var item in preVSaldos)
                {
                    // SUM(nvl(CASE WHEN ( :P_FINANCIADO_ID = 92 OR NVL(:P_FINANCIADO_ID,0) = 0) THEN DECODE(A.FINANCIADO_ID,719,0,C.MODIFICADO) ELSE C.MODIFICADO END,0)) MODIFICADO,
                    var preVSaldosDiarios = await _context.PRE_SALDOS_DIARIOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto && x.CODIGO_SALDO == item.CODIGO_SALDO && x.FECHA_SALDO <= fechaHasta).ToListAsync();
                    if (preVSaldosDiarios.Count > 0)
                    {
                        foreach (var itemDiario in preVSaldosDiarios)
                        {
                            if (financiadoId == 92 || financiadoId == 0)
                            {
                                if (item.FINANCIADO_ID == 719)
                                {
                                    result = result + 0;
                                }
                                else
                                {
                                    result = result + itemDiario.CAUSADO;
                                }

                            }
                            else
                            {
                                result = result + itemDiario.CAUSADO;
                            }
                        }
                    }

                }
            }

            return result;
        }
            public async Task<decimal> GetPagadoNivelUno(List<PRE_V_SALDOS> saldos, int financiadoId, DateTime fechaHasta, int codigoPresupuesto, string codigoGrupo, string codigoPartida)
        {

            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();

            if (financiadoId > 0) {
                preVSaldos = saldos
                  .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                              x.FINANCIADO_ID == financiadoId &&
                              x.CODIGO_GRUPO == codigoGrupo &&
                              x.CODIGO_PARTIDA == codigoPartida).ToList();
            }
            else {
                preVSaldos = saldos
                      .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                 
                                  x.CODIGO_GRUPO == codigoGrupo &&
                                  x.CODIGO_PARTIDA == codigoPartida).ToList();
            }
          

            if (preVSaldos.Count > 0)
            {
                foreach (var item in preVSaldos)
                {
                    // SUM(nvl(CASE WHEN ( :P_FINANCIADO_ID = 92 OR NVL(:P_FINANCIADO_ID,0) = 0) THEN DECODE(A.FINANCIADO_ID,719,0,C.MODIFICADO) ELSE C.MODIFICADO END,0)) MODIFICADO,
                    var preVSaldosDiarios = await _context.PRE_SALDOS_DIARIOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto && x.CODIGO_SALDO == item.CODIGO_SALDO && x.FECHA_SALDO <= fechaHasta).ToListAsync();
                    if (preVSaldosDiarios.Count > 0)
                    {
                        foreach (var itemDiario in preVSaldosDiarios)
                        {
                            if (financiadoId == 92 || financiadoId == 0)
                            {
                                if (item.FINANCIADO_ID == 719)
                                {
                                    result = result + 0;
                                }
                                else
                                {
                                    result = result + itemDiario.PAGADO;
                                }

                            }
                            else
                            {
                                result = result + itemDiario.PAGADO;
                            }
                        }
                    }

                }
            }

            return result;
        }
            public async Task<decimal> GetAsignacionNivelUno(List<PRE_V_SALDOS> saldos, int codigoPresupuesto, string codigoGrupo, string codigoPartida)
            {
                decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();

            preVSaldos = saldos
                        .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                    x.CODIGO_GRUPO == codigoGrupo &&
                                    x.CODIGO_PARTIDA == codigoPartida).ToList();
          
            var resumen = from s in preVSaldos
                          group s by new { CodigoPresupuesto = s.CODIGO_PRESUPUESTO } into g
                          select new
                          {
                              g.Key.CodigoPresupuesto,
                              ASIGNACION = g.Sum(s => s.ASIGNACION),
                           
                          };
            if (resumen.Count()>0)
            {
                var itemResumen = resumen.FirstOrDefault();
                if (itemResumen != null)
                {
                    result = itemResumen.ASIGNACION;
                }
               
               
            }
            //var finalre = DateTime.Now;
            //var diffre = finalre - iniciore;

            return result;
        
            }
        #endregion


        #region NivelDos

        public async Task<decimal> GetPresupuestadoNivelDos(List<PRE_V_SALDOS> saldos, int codigoPresupuesto,int financiadoId, string codigoGrupo, string codigoPartida, string codigoGenerica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) {
                preVSaldos = saldos
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                x.FINANCIADO_ID==financiadoId &&
                                x.CODIGO_GRUPO == codigoGrupo &&
                                x.CODIGO_PARTIDA == codigoPartida &&
                                x.CODIGO_GENERICA == codigoGenerica).ToList();
            }
            else {
                preVSaldos = saldos
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                x.CODIGO_GRUPO == codigoGrupo &&
                                x.CODIGO_PARTIDA == codigoPartida &&
                                x.CODIGO_GENERICA == codigoGenerica).ToList();
            }
            

            if (preVSaldos.Count > 0)
            {
                /*foreach (var item in preVSaldos)
                {
                    if (item.ASIGNACION >= 0)
                    {
                        result = result + item.PRESUPUESTADO;
                    }

                }*/
                var resumen = from s in preVSaldos.Where(x => x.ASIGNACION >= 0)
                              group s by new { CodigoPresupuesto = s.CODIGO_PRESUPUESTO } into g
                              select new
                              {
                                  g.Key.CodigoPresupuesto,

                                  PRESUPUESTADO = g.Sum(s => s.PRESUPUESTADO),

                              };
                if (resumen.Count() > 0)
                {
                    var itemResumen = resumen.FirstOrDefault();
                    if (itemResumen != null)
                    {
                        result = itemResumen.PRESUPUESTADO;
                    }


                }
            }
            return result;
        }
        public async Task<decimal> GetModificadoNivelDos(List<PRE_V_SALDOS> saldos, int financiadoId, DateTime fechaHasta, int codigoPresupuesto, string codigoGrupo, string codigoPartida, string codigoGenerica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) {
                preVSaldos = saldos
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                x.FINANCIADO_ID == financiadoId &&
                                x.CODIGO_GRUPO == codigoGrupo &&
                                x.CODIGO_PARTIDA == codigoPartida &&
                                x.CODIGO_GENERICA == codigoGenerica).ToList();
            }
            else {
                preVSaldos = saldos
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               
                                x.CODIGO_GRUPO == codigoGrupo &&
                                x.CODIGO_PARTIDA == codigoPartida &&
                                x.CODIGO_GENERICA == codigoGenerica).ToList();
            }
            

            if (preVSaldos.Count > 0)
            {
                foreach (var item in preVSaldos)
                {
                    // SUM(nvl(CASE WHEN ( :P_FINANCIADO_ID = 92 OR NVL(:P_FINANCIADO_ID,0) = 0) THEN DECODE(A.FINANCIADO_ID,719,0,C.MODIFICADO) ELSE C.MODIFICADO END,0)) MODIFICADO,
                    var preVSaldosDiarios = await _context.PRE_SALDOS_DIARIOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto && x.CODIGO_SALDO == item.CODIGO_SALDO && x.FECHA_SALDO <= fechaHasta).ToListAsync();
                    if (preVSaldosDiarios.Count > 0)
                    {
                       /* foreach (var itemDiario in preVSaldosDiarios)
                        {
                            if (finaciadoId == 92 || finaciadoId == 0)
                            {
                                if (item.FINANCIADO_ID == 719)
                                {
                                    result = result + 0;
                                }
                                else
                                {
                                    result = result + itemDiario.MODIFICADO;
                                }

                            }
                            else
                            {
                                result = result + itemDiario.MODIFICADO;
                            }
                        }*/

                        var resumen = from s in preVSaldos
                                      group s by new { CodigoPresupuesto = s.CODIGO_PRESUPUESTO } into g
                                      select new
                                      {
                                          g.Key.CodigoPresupuesto,

                                          MODIFICADO = g.Sum(s => s.MODIFICADO),

                                      };
                        if (resumen.Count() > 0)
                        {
                            var itemResumen = resumen.FirstOrDefault();
                            if (itemResumen != null)
                            {
                                result = itemResumen.MODIFICADO;
                            }


                        }
                    }


                }
            }
            return result;
        }
        public async Task<decimal> GetComprometidoNivelDos(List<PRE_V_SALDOS> saldos, int financiadoId, int codigoPresupuesto, string codigoGrupo, string codigoPartida, string codigoGenerica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId>0) {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               x.FINANCIADO_ID == financiadoId &&
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica).ToList();
            }
            else {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                             
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica).ToList();
            }
           

            if (preVSaldos.Count > 0)
            {
                foreach (var item in preVSaldos)
                {
                    if (financiadoId == 92 || financiadoId == 0)
                    {
                        if (item.FINANCIADO_ID == 719)
                        {
                            result = result + 0;
                        }
                        else
                        {
                            result = result + item.COMPROMETIDO;
                        }

                    }
                    else
                    {
                        result = result + item.COMPROMETIDO;
                    }

                }
            }
            return result;
        }
        public async Task<decimal> GetBloqueadoNivelDos(List<PRE_V_SALDOS> saldos, int financiadoId, int codigoPresupuesto, string codigoGrupo, string codigoPartida, string codigoGenerica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) { 
                preVSaldos = saldos
                  .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                              x.FINANCIADO_ID == financiadoId &&
                              x.CODIGO_GRUPO == codigoGrupo &&
                              x.CODIGO_PARTIDA == codigoPartida &&
                              x.CODIGO_GENERICA == codigoGenerica).ToList();
            }
            else {
                preVSaldos = saldos
                  .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                            
                              x.CODIGO_GRUPO == codigoGrupo &&
                              x.CODIGO_PARTIDA == codigoPartida &&
                              x.CODIGO_GENERICA == codigoGenerica).ToList();
            }
          

            if (preVSaldos.Count > 0)
            {
                /*foreach (var item in preVSaldos)
                {
                    result = result + item.BLOQUEADO;

                }*/
                var resumen = from s in preVSaldos
                              group s by new { CodigoPresupuesto = s.CODIGO_PRESUPUESTO } into g
                              select new
                              {
                                  g.Key.CodigoPresupuesto,

                                  BLOQUEADO = g.Sum(s => s.BLOQUEADO),

                              };
                if (resumen.Count() > 0)
                {
                    var itemResumen = resumen.FirstOrDefault();
                    if (itemResumen != null)
                    {
                        result = itemResumen.BLOQUEADO;
                    }


                }
            }
            return result;
        }
        public async Task<decimal> GetCausadoNivelDos(List<PRE_V_SALDOS> saldos, int financiadoId, DateTime fechaHasta, int codigoPresupuesto, string codigoGrupo, string codigoPartida, string codigoGenerica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               x.FINANCIADO_ID == financiadoId &&
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica).ToList();
            }
            else {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                            
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica).ToList();

            }
           

            if (preVSaldos.Count > 0)
            {
                foreach (var item in preVSaldos)
                {
                    // SUM(nvl(CASE WHEN ( :P_FINANCIADO_ID = 92 OR NVL(:P_FINANCIADO_ID,0) = 0) THEN DECODE(A.FINANCIADO_ID,719,0,C.MODIFICADO) ELSE C.MODIFICADO END,0)) MODIFICADO,
                    var preVSaldosDiarios = await _context.PRE_SALDOS_DIARIOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto && x.CODIGO_SALDO == item.CODIGO_SALDO && x.FECHA_SALDO <= fechaHasta).ToListAsync();
                    if (preVSaldosDiarios.Count > 0)
                    {
                        foreach (var itemDiario in preVSaldosDiarios)
                        {
                            if (financiadoId == 92 || financiadoId == 0)
                            {
                                if (item.FINANCIADO_ID == 719)
                                {
                                    result = result + 0;
                                }
                                else
                                {
                                    result = result + itemDiario.CAUSADO;
                                }

                            }
                            else
                            {
                                result = result + itemDiario.CAUSADO;
                            }
                        }
                    }


                }
            }
            return result;
        }
        public async Task<decimal> GetPagadoNivelDos(List<PRE_V_SALDOS> saldos, int financiadoId, DateTime fechaHasta, int codigoPresupuesto, string codigoGrupo, string codigoPartida, string codigoGenerica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) {
                preVSaldos = saldos
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                x.FINANCIADO_ID == financiadoId &&
                                x.CODIGO_GRUPO == codigoGrupo &&
                                x.CODIGO_PARTIDA == codigoPartida &&
                                x.CODIGO_GENERICA == codigoGenerica).ToList();
            }
            else {
                preVSaldos = saldos
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               
                                x.CODIGO_GRUPO == codigoGrupo &&
                                x.CODIGO_PARTIDA == codigoPartida &&
                                x.CODIGO_GENERICA == codigoGenerica).ToList();
            }
           

            if (preVSaldos.Count > 0)
            {
                foreach (var item in preVSaldos)
                {
                    // SUM(nvl(CASE WHEN ( :P_FINANCIADO_ID = 92 OR NVL(:P_FINANCIADO_ID,0) = 0) THEN DECODE(A.FINANCIADO_ID,719,0,C.MODIFICADO) ELSE C.MODIFICADO END,0)) MODIFICADO,
                    var preVSaldosDiarios = await _context.PRE_SALDOS_DIARIOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto && x.CODIGO_SALDO == item.CODIGO_SALDO && x.FECHA_SALDO <= fechaHasta).ToListAsync();
                    if (preVSaldosDiarios.Count > 0)
                    {
                        foreach (var itemDiario in preVSaldosDiarios)
                        {
                            if (financiadoId == 92 || financiadoId == 0)
                            {
                                if (item.FINANCIADO_ID == 719)
                                {
                                    result = result + 0;
                                }
                                else
                                {
                                    result = result + itemDiario.PAGADO;
                                }

                            }
                            else
                            {
                                result = result + itemDiario.PAGADO;
                            }
                        }
                    }


                }
            }
            return result;
        }
        public async Task<decimal> GetAsignacionNivelDos(List<PRE_V_SALDOS> saldos, int codigoPresupuesto,int financiadoId, string codigoGrupo, string codigoPartida, string codigoGenerica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) { 
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               x.FINANCIADO_ID==financiadoId &&
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica).ToList();

            }
            else {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica).ToList();

            }

            if (preVSaldos.Count > 0)
            {
                /*foreach (var item in preVSaldos)
                {
                 
                        result = result + item.ASIGNACION;
                   

                }*/
                var resumen = from s in preVSaldos
                              group s by new { CodigoPresupuesto = s.CODIGO_PRESUPUESTO } into g
                              select new
                              {
                                  g.Key.CodigoPresupuesto,

                                  ASIGNACION = g.Sum(s => s.ASIGNACION),

                              };
                if (resumen.Count() > 0)
                {
                    var itemResumen = resumen.FirstOrDefault();
                    if (itemResumen != null)
                    {
                        result = itemResumen.ASIGNACION;
                    }


                }
            }
            return result;
        }
        #endregion

        #region NivelTres

        public async Task<decimal> GetPresupuestadoNivelTres(List<PRE_V_SALDOS> saldos, int codigoPresupuesto,int financiadoId, string codigoGrupo, string codigoPartida, string codigoGenerica, string codigoEspecifica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                   x.FINANCIADO_ID==financiadoId &&
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica &&
                               x.CODIGO_ESPECIFICA == codigoEspecifica).ToList();
            }
            else {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica &&
                               x.CODIGO_ESPECIFICA == codigoEspecifica).ToList();
            }
           

            if (preVSaldos.Count > 0)
            {
               

                var resumen = from s in preVSaldos.Where(x => x.ASIGNACION >= 0)
                              group s by new { CodigoPresupuesto = s.CODIGO_PRESUPUESTO } into g
                              select new
                              {
                                  g.Key.CodigoPresupuesto,

                                  PRESUPUESTADO = g.Sum(s => s.PRESUPUESTADO),

                              };
                if (resumen.Count() > 0)
                {
                    var itemResumen = resumen.FirstOrDefault();
                    if (itemResumen != null)
                    {
                        result = itemResumen.PRESUPUESTADO;
                    }


                }
            }
            return result;
        }
        public async Task<decimal> GetModificadoNivelTres(List<PRE_V_SALDOS> saldos, int financiadoId, DateTime fechadesde, DateTime fechaHasta, int codigoPresupuesto, string codigoGrupo, string codigoPartida, string codigoGenerica, string codigoEspecifica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               x.FINANCIADO_ID == financiadoId &&
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica &&
                               x.CODIGO_ESPECIFICA == codigoEspecifica).ToList();
            }
            else {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                             
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica &&
                               x.CODIGO_ESPECIFICA == codigoEspecifica).ToList();
            }
          

            if (preVSaldos.Count > 0)
            {
                foreach (var item in preVSaldos)
                {
                    //         SUM(nvl(CASE WHEN ( :P_FINANCIADO_ID = 92 OR NVL(:P_FINANCIADO_ID,0) = 0) THEN DECODE(A.FINANCIADO_ID,719,0,C.MODIFICADO) ELSE C.MODIFICADO END,0)) MODIFICADO,

                    var preVSaldosDiarios = await _context.PRE_SALDOS_DIARIOS.DefaultIfEmpty()
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto && x.CODIGO_SALDO == item.CODIGO_SALDO && x.FECHA_SALDO >= fechadesde && x.FECHA_SALDO <= fechaHasta).ToListAsync();

                    if (preVSaldosDiarios.Count > 0)
                    {
                       
                        var resumen = from s in preVSaldos
                                      group s by new { CodigoPresupuesto = s.CODIGO_PRESUPUESTO } into g
                                      select new
                                      {
                                          g.Key.CodigoPresupuesto,

                                          MODIFICADO = g.Sum(s => s.MODIFICADO),

                                      };
                        if (resumen.Count() > 0)
                        {
                            var itemResumen = resumen.FirstOrDefault();
                            if (itemResumen != null)
                            {
                                result = itemResumen.MODIFICADO;
                            }


                        }
                    }


                }
            }
            return result;
        }
        public async Task<decimal> GetComprometidoNivelTres(List<PRE_V_SALDOS> saldos, int financiadoId, int codigoPresupuesto, string codigoGrupo, string codigoPartida, string codigoGenerica, string codigoEspecifica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               x.FINANCIADO_ID == financiadoId &&
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica &&
                               x.CODIGO_ESPECIFICA == codigoEspecifica).ToList();
            }
            else {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                             
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica &&
                               x.CODIGO_ESPECIFICA == codigoEspecifica).ToList();
            }
        

            if (preVSaldos.Count > 0)
            {
                foreach (var item in preVSaldos)
                {

                    if (financiadoId == 92 || financiadoId == 0)
                    {
                        if (item.FINANCIADO_ID == 719)
                        {
                            result = result + 0;
                        }
                        else
                        {
                            result = result + item.COMPROMETIDO;
                        }

                    }
                    else
                    {
                        result = result + item.COMPROMETIDO;
                    }

                }
            }
            return result;
        }
        public async Task<decimal> GetBloqueadoNivelTres(List<PRE_V_SALDOS> saldos, int financiadoId, int codigoPresupuesto, string codigoGrupo, string codigoPartida, string codigoGenerica, string codigoEspecifica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) {
                preVSaldos = saldos
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                x.FINANCIADO_ID == financiadoId &&
                                x.CODIGO_GRUPO == codigoGrupo &&
                                x.CODIGO_PARTIDA == codigoPartida &&
                                x.CODIGO_GENERICA == codigoGenerica &&
                                x.CODIGO_ESPECIFICA == codigoEspecifica).ToList();
            }
            else {

                preVSaldos = saldos
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               
                                x.CODIGO_GRUPO == codigoGrupo &&
                                x.CODIGO_PARTIDA == codigoPartida &&
                                x.CODIGO_GENERICA == codigoGenerica &&
                                x.CODIGO_ESPECIFICA == codigoEspecifica).ToList();
            }
            

            if (preVSaldos.Count > 0)
            {
               
                var resumen = from s in preVSaldos
                              group s by new { CodigoPresupuesto = s.CODIGO_PRESUPUESTO } into g
                              select new
                              {
                                  g.Key.CodigoPresupuesto,

                                  BLOQUEADO = g.Sum(s => s.BLOQUEADO),

                              };
                if (resumen.Count() > 0)
                {
                    var itemResumen = resumen.FirstOrDefault();
                    if (itemResumen != null)
                    {
                        result = itemResumen.BLOQUEADO;
                    }


                }
            }
            return result;
        }
        public async Task<decimal> GetCausadoNivelTres(List<PRE_V_SALDOS> saldos, int financiadoId, DateTime fechadesde, DateTime fechaHasta, int codigoPresupuesto, string codigoGrupo, string codigoPartida, string codigoGenerica, string codigoEspecifica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               x.FINANCIADO_ID == financiadoId &&
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica &&
                               x.CODIGO_ESPECIFICA == codigoEspecifica).ToList();
            }
            else {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                             
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica &&
                               x.CODIGO_ESPECIFICA == codigoEspecifica).ToList();
            }
           

            if (preVSaldos.Count > 0)
            {
                foreach (var item in preVSaldos)
                {
                    //         SUM(nvl(CASE WHEN ( :P_FINANCIADO_ID = 92 OR NVL(:P_FINANCIADO_ID,0) = 0) THEN DECODE(A.FINANCIADO_ID,719,0,C.MODIFICADO) ELSE C.MODIFICADO END,0)) MODIFICADO,

                    var preVSaldosDiarios = await _context.PRE_SALDOS_DIARIOS.DefaultIfEmpty()
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto && x.CODIGO_SALDO == item.CODIGO_SALDO && x.FECHA_SALDO >= fechadesde && x.FECHA_SALDO <= fechaHasta).ToListAsync();

                    if (preVSaldosDiarios.Count > 0)
                    {
                        foreach (var itemDiario in preVSaldosDiarios)
                        {
                            if (financiadoId == 92 || financiadoId == 0)
                            {
                                if (item.FINANCIADO_ID == 719)
                                {
                                    result = result + 0;
                                }
                                else
                                {
                                    result = result + itemDiario.CAUSADO;
                                }

                            }
                            else
                            {
                                result = result + itemDiario.CAUSADO;
                            }
                        }
                    }


                }
            }
            return result;
        }
        public async Task<decimal> GetPagadoNivelTres(List<PRE_V_SALDOS> saldos, int financiadoId, DateTime fechadesde, DateTime fechaHasta, int codigoPresupuesto, string codigoGrupo, string codigoPartida, string codigoGenerica, string codigoEspecifica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) {
                preVSaldos = saldos
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                x.FINANCIADO_ID == financiadoId &&
                                x.CODIGO_GRUPO == codigoGrupo &&
                                x.CODIGO_PARTIDA == codigoPartida &&
                                x.CODIGO_GENERICA == codigoGenerica &&
                                x.CODIGO_ESPECIFICA == codigoEspecifica).ToList();
            }
            else {
                preVSaldos = saldos
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                          
                                x.CODIGO_GRUPO == codigoGrupo &&
                                x.CODIGO_PARTIDA == codigoPartida &&
                                x.CODIGO_GENERICA == codigoGenerica &&
                                x.CODIGO_ESPECIFICA == codigoEspecifica).ToList();

            }
            

            if (preVSaldos.Count > 0)
            {
                foreach (var item in preVSaldos)
                {
                    //         SUM(nvl(CASE WHEN ( :P_FINANCIADO_ID = 92 OR NVL(:P_FINANCIADO_ID,0) = 0) THEN DECODE(A.FINANCIADO_ID,719,0,C.MODIFICADO) ELSE C.MODIFICADO END,0)) MODIFICADO,

                    var preVSaldosDiarios = await _context.PRE_SALDOS_DIARIOS.DefaultIfEmpty()
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto && x.CODIGO_SALDO == item.CODIGO_SALDO && x.FECHA_SALDO >= fechadesde && x.FECHA_SALDO <= fechaHasta).ToListAsync();

                    if (preVSaldosDiarios.Count > 0)
                    {
                        foreach (var itemDiario in preVSaldosDiarios)
                        {
                            if (financiadoId == 92 || financiadoId == 0)
                            {
                                if (item.FINANCIADO_ID == 719)
                                {
                                    result = result + 0;
                                }
                                else
                                {
                                    result = result + itemDiario.PAGADO;
                                }

                            }
                            else
                            {
                                result = result + itemDiario.PAGADO;
                            }
                        }
                    }


                }
            }
            return result;
        }
        public async Task<decimal> GetAsignacionNivelTres(List<PRE_V_SALDOS> saldos, int codigoPresupuesto,int financiadoId, string codigoGrupo, string codigoPartida, string codigoGenerica, string codigoEspecifica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) {
                preVSaldos = saldos
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                x.FINANCIADO_ID==financiadoId &&
                                x.CODIGO_GRUPO == codigoGrupo &&
                                x.CODIGO_PARTIDA == codigoPartida &&
                                x.CODIGO_GENERICA == codigoGenerica &&
                                x.CODIGO_ESPECIFICA == codigoEspecifica).ToList();
            }
            else {
                preVSaldos = saldos
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                x.CODIGO_GRUPO == codigoGrupo &&
                                x.CODIGO_PARTIDA == codigoPartida &&
                                x.CODIGO_GENERICA == codigoGenerica &&
                                x.CODIGO_ESPECIFICA == codigoEspecifica).ToList();
            }
            

            if (preVSaldos.Count > 0)
            {
                /*foreach (var item in preVSaldos)
                {
                 
                        result = result + item.ASIGNACION;
                   

                }*/
                var resumen = from s in preVSaldos
                              group s by new { CodigoPresupuesto = s.CODIGO_PRESUPUESTO } into g
                              select new
                              {
                                  g.Key.CodigoPresupuesto,

                                  ASIGNACION = g.Sum(s => s.ASIGNACION),

                              };
                if (resumen.Count() > 0)
                {
                    var itemResumen = resumen.FirstOrDefault();
                    if (itemResumen != null)
                    {
                        result = itemResumen.ASIGNACION;
                    }


                }
            }
            return result;
        }
       #endregion

        #region NivelCuatro

        public async Task<decimal> GetPresupuestadoNivelCuatro(List<PRE_V_SALDOS> saldos, int codigoPresupuesto,int financiadoId, string codigoGrupo, string codigoPartida, string codigoGenerica, string codigoEspecifica, string codigoSubEspecifica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               x.FINANCIADO_ID==financiadoId &&
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica &&
                               x.CODIGO_ESPECIFICA == codigoEspecifica &&
                               x.CODIGO_SUBESPECIFICA == codigoSubEspecifica).ToList();
            }
            else {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica &&
                               x.CODIGO_ESPECIFICA == codigoEspecifica &&
                               x.CODIGO_SUBESPECIFICA == codigoSubEspecifica).ToList();
            }
            

            if (preVSaldos.Count > 0)
            {
                /*foreach (var item in preVSaldos)
                {
                    if (item.ASIGNACION >= 0)
                    {
                        result = result + item.PRESUPUESTADO;
                    }

                }*/
                var resumen = from s in preVSaldos.Where(x => x.ASIGNACION >= 0)
                              group s by new { CodigoPresupuesto = s.CODIGO_PRESUPUESTO } into g
                              select new
                              {
                                  g.Key.CodigoPresupuesto,

                                  PRESUPUESTADO = g.Sum(s => s.PRESUPUESTADO),

                              };
                if (resumen.Count() > 0)
                {
                    var itemResumen = resumen.FirstOrDefault();
                    if (itemResumen != null)
                    {
                        result = itemResumen.PRESUPUESTADO;
                    }


                }
            }
            return result;
        }
        public async Task<decimal> GetModificadoNivelCuatro(List<PRE_V_SALDOS> saldos, int financiadoId, DateTime fechaHasta, int codigoPresupuesto, string codigoGrupo, string codigoPartida, string codigoGenerica, string codigoEspecifica, string codigoSubEspecifica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) {
                preVSaldos = saldos
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                x.FINANCIADO_ID == financiadoId &&
                                x.CODIGO_GRUPO == codigoGrupo &&
                                x.CODIGO_PARTIDA == codigoPartida &&
                                x.CODIGO_GENERICA == codigoGenerica &&
                                x.CODIGO_ESPECIFICA == codigoEspecifica &&
                                x.CODIGO_SUBESPECIFICA == codigoSubEspecifica).ToList();
            }
            else {
                preVSaldos = saldos
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                             
                                x.CODIGO_GRUPO == codigoGrupo &&
                                x.CODIGO_PARTIDA == codigoPartida &&
                                x.CODIGO_GENERICA == codigoGenerica &&
                                x.CODIGO_ESPECIFICA == codigoEspecifica &&
                                x.CODIGO_SUBESPECIFICA == codigoSubEspecifica).ToList();
            }
           

            if (preVSaldos.Count > 0)
            {
                foreach (var item in preVSaldos)
                {
                    //         SUM(nvl(CASE WHEN ( :P_FINANCIADO_ID = 92 OR NVL(:P_FINANCIADO_ID,0) = 0) THEN DECODE(A.FINANCIADO_ID,719,0,C.MODIFICADO) ELSE C.MODIFICADO END,0)) MODIFICADO,
                    //         SUM(nvl(CASE WHEN( :P_FINANCIADO_ID = 92 OR NVL(:P_FINANCIADO_ID, 0) = 0) THEN DECODE(A.FINANCIADO_ID, 719, 0, C.MODIFICADO) ELSE C.MODIFICADO END, 0)) MODIFICADO,


                    var preVSaldosDiarios = await _context.PRE_SALDOS_DIARIOS.DefaultIfEmpty()
                        .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto && x.CODIGO_SALDO == item.CODIGO_SALDO && x.FECHA_SALDO <= fechaHasta).ToListAsync();
                    if (preVSaldosDiarios.Count > 0)
                    {
                       
                        var resumen = from s in preVSaldos
                                      group s by new { CodigoPresupuesto = s.CODIGO_PRESUPUESTO } into g
                                      select new
                                      {
                                          g.Key.CodigoPresupuesto,

                                          MODIFICADO = g.Sum(s => s.MODIFICADO),

                                      };
                        if (resumen.Count() > 0)
                        {
                            var itemResumen = resumen.FirstOrDefault();
                            if (itemResumen != null)
                            {
                                result = itemResumen.MODIFICADO;
                            }


                        }
                    }


                }
            }
            return result;
        }
        public async Task<decimal> GetComprometidoNivelCuatro(List<PRE_V_SALDOS> saldos, int financiadoId, int codigoPresupuesto, string codigoGrupo, string codigoPartida, string codigoGenerica, string codigoEspecifica, string codigoSubEspecifica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               x.FINANCIADO_ID == financiadoId &&
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica &&
                               x.CODIGO_ESPECIFICA == codigoEspecifica &&
                               x.CODIGO_SUBESPECIFICA == codigoSubEspecifica).ToList();
            }
            else {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                            
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica &&
                               x.CODIGO_ESPECIFICA == codigoEspecifica &&
                               x.CODIGO_SUBESPECIFICA == codigoSubEspecifica).ToList();
            }
          

            if (preVSaldos.Count > 0)
            {
                foreach (var item in preVSaldos)
                {

                    if (financiadoId == 92 || financiadoId == 0)
                    {
                        if (item.FINANCIADO_ID == 719)
                        {
                            result = result + 0;
                        }
                        else
                        {
                            result = result + item.COMPROMETIDO;
                        }

                    }
                    else
                    {
                        result = result + item.COMPROMETIDO;
                    }

                }
            }
            return result;
        }
        public async Task<decimal> GetBloqueadoNivelCuatro(List<PRE_V_SALDOS> saldos, int financiadoId, int codigoPresupuesto, string codigoGrupo, string codigoPartida, string codigoGenerica, string codigoEspecifica, string codigoSubEspecifica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) {
                preVSaldos = saldos
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                x.FINANCIADO_ID == financiadoId &&
                                x.CODIGO_GRUPO == codigoGrupo &&
                                x.CODIGO_PARTIDA == codigoPartida &&
                                x.CODIGO_GENERICA == codigoGenerica &&
                                x.CODIGO_ESPECIFICA == codigoEspecifica &&
                                x.CODIGO_SUBESPECIFICA == codigoSubEspecifica).ToList();
            }
            else {
                preVSaldos = saldos
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               
                                x.CODIGO_GRUPO == codigoGrupo &&
                                x.CODIGO_PARTIDA == codigoPartida &&
                                x.CODIGO_GENERICA == codigoGenerica &&
                                x.CODIGO_ESPECIFICA == codigoEspecifica &&
                                x.CODIGO_SUBESPECIFICA == codigoSubEspecifica).ToList();
            }
            

            if (preVSaldos.Count > 0)
            {
                /*foreach (var item in preVSaldos)
                {
                    result = result + item.BLOQUEADO;
                   

                }*/
                var resumen = from s in preVSaldos
                              group s by new { CodigoPresupuesto = s.CODIGO_PRESUPUESTO } into g
                              select new
                              {
                                  g.Key.CodigoPresupuesto,

                                  BLOQUEADO = g.Sum(s => s.BLOQUEADO),

                              };
                if (resumen.Count() > 0)
                {
                    var itemResumen = resumen.FirstOrDefault();
                    if (itemResumen != null)
                    {
                        result = itemResumen.BLOQUEADO;
                    }


                }
            }
            return result;
        }
        public async Task<decimal> GetCausadoNivelCuatro(List<PRE_V_SALDOS> saldos, int financiadoId, DateTime fechaHasta, int codigoPresupuesto, string codigoGrupo, string codigoPartida, string codigoGenerica, string codigoEspecifica, string codigoSubEspecifica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId>0) {
                preVSaldos = saldos
                 .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                             x.FINANCIADO_ID == financiadoId &&
                             x.CODIGO_GRUPO == codigoGrupo &&
                             x.CODIGO_PARTIDA == codigoPartida &&
                             x.CODIGO_GENERICA == codigoGenerica &&
                             x.CODIGO_ESPECIFICA == codigoEspecifica &&
                             x.CODIGO_SUBESPECIFICA == codigoSubEspecifica).ToList();
            }
            else {
                preVSaldos = saldos
                 .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                           
                             x.CODIGO_GRUPO == codigoGrupo &&
                             x.CODIGO_PARTIDA == codigoPartida &&
                             x.CODIGO_GENERICA == codigoGenerica &&
                             x.CODIGO_ESPECIFICA == codigoEspecifica &&
                             x.CODIGO_SUBESPECIFICA == codigoSubEspecifica).ToList();
            }
         

            if (preVSaldos.Count > 0)
            {
                foreach (var item in preVSaldos)
                {
                    //         SUM(nvl(CASE WHEN ( :P_FINANCIADO_ID = 92 OR NVL(:P_FINANCIADO_ID,0) = 0) THEN DECODE(A.FINANCIADO_ID,719,0,C.MODIFICADO) ELSE C.MODIFICADO END,0)) MODIFICADO,
                    //         SUM(nvl(CASE WHEN( :P_FINANCIADO_ID = 92 OR NVL(:P_FINANCIADO_ID, 0) = 0) THEN DECODE(A.FINANCIADO_ID, 719, 0, C.MODIFICADO) ELSE C.MODIFICADO END, 0)) MODIFICADO,


                    var preVSaldosDiarios = await _context.PRE_SALDOS_DIARIOS.DefaultIfEmpty()
                        .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto && x.CODIGO_SALDO == item.CODIGO_SALDO && x.FECHA_SALDO <= fechaHasta).ToListAsync();
                    if (preVSaldosDiarios.Count > 0)
                    {
                        foreach (var itemDiario in preVSaldosDiarios)
                        {
                            if (financiadoId == 92 || financiadoId == 0)
                            {
                                if (item.FINANCIADO_ID == 719)
                                {
                                    result = result + 0;
                                }
                                else
                                {
                                    result = result + itemDiario.CAUSADO;
                                }

                            }
                            else
                            {
                                result = result + itemDiario.CAUSADO;
                            }
                        }
                    }


                }
            }
            return result;
        }
        public async Task<decimal> GetPagadoNivelCuatro(List<PRE_V_SALDOS> saldos, int financiadoId, DateTime fechaHasta, int codigoPresupuesto, string codigoGrupo, string codigoPartida, string codigoGenerica, string codigoEspecifica, string codigoSubEspecifica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId > 0) {
                preVSaldos = saldos
                 .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                             x.FINANCIADO_ID == financiadoId &&
                             x.CODIGO_GRUPO == codigoGrupo &&
                             x.CODIGO_PARTIDA == codigoPartida &&
                             x.CODIGO_GENERICA == codigoGenerica &&
                             x.CODIGO_ESPECIFICA == codigoEspecifica &&
                             x.CODIGO_SUBESPECIFICA == codigoSubEspecifica).ToList();
            }
            else {
                preVSaldos = saldos
                 .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                           
                             x.CODIGO_GRUPO == codigoGrupo &&
                             x.CODIGO_PARTIDA == codigoPartida &&
                             x.CODIGO_GENERICA == codigoGenerica &&
                             x.CODIGO_ESPECIFICA == codigoEspecifica &&
                             x.CODIGO_SUBESPECIFICA == codigoSubEspecifica).ToList();
            }
         

            if (preVSaldos.Count > 0)
            {
                foreach (var item in preVSaldos)
                {
                    //         SUM(nvl(CASE WHEN ( :P_FINANCIADO_ID = 92 OR NVL(:P_FINANCIADO_ID,0) = 0) THEN DECODE(A.FINANCIADO_ID,719,0,C.MODIFICADO) ELSE C.MODIFICADO END,0)) MODIFICADO,
                    //         SUM(nvl(CASE WHEN( :P_FINANCIADO_ID = 92 OR NVL(:P_FINANCIADO_ID, 0) = 0) THEN DECODE(A.FINANCIADO_ID, 719, 0, C.MODIFICADO) ELSE C.MODIFICADO END, 0)) MODIFICADO,


                    var preVSaldosDiarios = await _context.PRE_SALDOS_DIARIOS.DefaultIfEmpty()
                        .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto && x.CODIGO_SALDO == item.CODIGO_SALDO && x.FECHA_SALDO <= fechaHasta).ToListAsync();
                    if (preVSaldosDiarios.Count > 0)
                    {
                        foreach (var itemDiario in preVSaldosDiarios)
                        {
                            if (financiadoId == 92 || financiadoId == 0)
                            {
                                if (item.FINANCIADO_ID == 719)
                                {
                                    result = result + 0;
                                }
                                else
                                {
                                    result = result + itemDiario.PAGADO;
                                }

                            }
                            else
                            {
                                result = result + itemDiario.PAGADO;
                            }
                        }
                    }


                }
            }
            return result;
        }
        public async Task<decimal> GetAsignacionNivelCuatro(List<PRE_V_SALDOS> saldos, int codigoPresupuesto, int financiadoId,string codigoGrupo, string codigoPartida, string codigoGenerica, string codigoEspecifica, string codigoSubEspecifica)
        {
            decimal result = 0;
            List<PRE_V_SALDOS> preVSaldos = new List<PRE_V_SALDOS>();
            if (financiadoId>0) {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               x.FINANCIADO_ID==financiadoId &&
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica &&
                               x.CODIGO_ESPECIFICA == codigoEspecifica &&
                               x.CODIGO_SUBESPECIFICA == codigoSubEspecifica).ToList();
            }
            else {
                preVSaldos = saldos
                   .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                               x.CODIGO_GRUPO == codigoGrupo &&
                               x.CODIGO_PARTIDA == codigoPartida &&
                               x.CODIGO_GENERICA == codigoGenerica &&
                               x.CODIGO_ESPECIFICA == codigoEspecifica &&
                               x.CODIGO_SUBESPECIFICA == codigoSubEspecifica).ToList();
            }
           

            if (preVSaldos.Count > 0)
            {
                /*foreach (var item in preVSaldos)
                {
                   
                        result = result + item.ASIGNACION;
                    

                }*/
                var resumen = from s in preVSaldos
                              group s by new { CodigoPresupuesto = s.CODIGO_PRESUPUESTO } into g
                              select new
                              {
                                  g.Key.CodigoPresupuesto,

                                  ASIGNACION = g.Sum(s => s.ASIGNACION),

                              };
                if (resumen.Count() > 0)
                {
                    var itemResumen = resumen.FirstOrDefault();
                    if (itemResumen != null)
                    {
                        result = itemResumen.ASIGNACION;
                    }


                }
            }
            return result;
        }
        #endregion










    }
}

