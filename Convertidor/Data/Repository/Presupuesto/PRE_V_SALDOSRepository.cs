using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Security.Policy;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using NuGet.Protocol.Core.Types;
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

        public async Task<List<PRE_V_SALDOS>> GetAllByPresupuestoPucConcat(FilterPresupuestoPucConcat filter)
        {
            try
            {

                var result = await _context.PRE_V_SALDOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto && x.CODIGO_PUC_CONCAT==filter.CodigoPucConcat).ToListAsync();
                return (List<PRE_V_SALDOS>)result;
            }
            catch (Exception ex)
            {

                return null;
            }

        }
        public List<PreDenominacionDto> GetResultDetail(List<AAPreDenominacionDto> preVSaldoAA, List<BBPreDenominacionDto> preVSaldoBB, List<CCPreDenominacionDto> preVSaldoCC)
        {
            List<PreDenominacionDto> resultDetail = new List<PreDenominacionDto>();
            foreach (var item in preVSaldoAA)
            {
                var preVSaldoBBDetail = preVSaldoBB.Where(x => x.CodigoSaldo == item.CodigoSaldo).FirstOrDefault();
                var preVSaldoCCDetal = preVSaldoCC.Where(x => x.CodigoSaldo == item.CodigoSaldo).FirstOrDefault();

                PreDenominacionDto resultDetailItem = new PreDenominacionDto();
                resultDetailItem.CodigoPresupuesto = item.CodigoPresupuesto;
                resultDetailItem.CodigoPartida = item.CodigoPartida;
                resultDetailItem.CodigoGenerica = item.CodigoGenerica;
                resultDetailItem.CodigoEspecifica = item.CodigoEspecifica;
                resultDetailItem.CodigoNivel5 = item.CodigoNivel5;

                resultDetailItem.DenominacionPuc = item.DenominacionPuc;
                resultDetailItem.Denominacion = item.Denominacion;
                resultDetailItem.Presupuestado = item.Presupuestado;
                //Buscar en CC
                resultDetailItem.Modificado = 0;
                if (preVSaldoCCDetal != null)
                {
                    resultDetailItem.Modificado = preVSaldoCCDetal.Modificado;

                }
                resultDetailItem.Vigente = 0;
                resultDetailItem.Comprometido = 0; //Buscar en BB
                if (preVSaldoBBDetail != null)
                {
                    resultDetailItem.Comprometido = preVSaldoBBDetail.Comprometido;

                }
                resultDetailItem.Bloqueado = item.Bloqueado;

                resultDetailItem.Causado = 0; //Buscar en BB
                if (preVSaldoBBDetail != null)
                {
                    resultDetailItem.Causado = preVSaldoBBDetail.Causado;

                }

                resultDetailItem.Pagado = 0; //Buscar en BB
                if (preVSaldoBBDetail != null)
                {
                    resultDetailItem.Pagado = preVSaldoBBDetail.Pagado;

                }

                resultDetailItem.Deuda = 0; //SUM(NVL(BB.COMPROMETIDO-BB.PAGADO,0)) DEUDA,
                if (preVSaldoBBDetail != null)
                {
                    resultDetailItem.Deuda = preVSaldoBBDetail.Comprometido - preVSaldoBBDetail.Pagado;

                }

                resultDetailItem.Disponibilidad = 0;// SUM(NVL(AA.PRESUPUESTADO,0)+NVL(CC.MODIFICADO,0))-SUM(NVL(CC.COMPROMETIDO,0)) DISPONIBILIDAD,
                if (preVSaldoCCDetal != null)
                {
                    resultDetailItem.Disponibilidad = item.Presupuestado + preVSaldoCCDetal.Modificado - preVSaldoCCDetal.Comprometido;

                }
                else
                {
                    resultDetailItem.Disponibilidad = item.Presupuestado;
                }

                resultDetailItem.Asignacion = item.Asignacion;

                resultDetailItem.DisponibilidadFinan = 0;//SUM(case when (NVL(AA.ASIGNACION-NVL(BB.COMPROMETIDO,0)+NVL(CC.MODIFICADO,0),0) ) > 0 then (NVL(AA.ASIGNACION-NVL(BB.COMPROMETIDO,0)+NVL(CC.MODIFICADO,0),0) ) else 0 end) DISPONIBILIDAD_FINAN
                if (preVSaldoCCDetal != null && preVSaldoBBDetail != null)
                {
                    if ((item.Asignacion - preVSaldoBBDetail.Comprometido) + preVSaldoCCDetal.Modificado > 0)
                    {
                        resultDetailItem.DisponibilidadFinan = (item.Asignacion - preVSaldoBBDetail.Comprometido) + preVSaldoCCDetal.Modificado;
                    }

                }
                resultDetail.Add(resultDetailItem);

            }

            return resultDetail;

        }

        public List<PreDenominacionDto> GetPreDenominacionDtoAgrupado(List<PreDenominacionDto> resultDetail)
        {

            //AGRUPAR POR
            /*AA.CODIGO_PARTIDA,
            AA.CODIGO_GENERICA,
            AA.CODIGO_ESPECIFICA,
            AA.CODIGO_SUBESPECIFICA,
            AA.CODIGO_NIVEL5,
            AA.DENOMINACION_PUC,
            AA.DENOMINACION*/
            //SUM DE:
            /*
                RESUPUESTADO,
                MODIFICADO,
                SUM(0) VIGENTE,       
                COMPROMETIDO,
                BLOQUEADO,
                CAUSADO,
                PAGADO,
                DEUDA,
                DISPONIBILIDAD,
                SIGNACION,
                DISPONIBILIDAD_FINAN
             */

            List<PreDenominacionDto> resultGROUP = resultDetail
                    .GroupBy(row => new { row.CodigoPresupuesto, row.CodigoPartida, row.CodigoGenerica, row.CodigoEspecifica, row.CodigoSubEspecifica, row.CodigoNivel5, row.DenominacionPuc, row.Denominacion })
                    .Select(g => new PreDenominacionDto()
                    {
                        CodigoPresupuesto = g.Key.CodigoPresupuesto,
                        CodigoPartida = g.Key.CodigoPartida,
                        CodigoGenerica = g.Key.CodigoGenerica,
                        CodigoEspecifica = g.Key.CodigoEspecifica,
                        CodigoSubEspecifica = g.Key.CodigoSubEspecifica,
                        CodigoNivel5 = g.Key.CodigoNivel5,
                        DenominacionPuc = g.Key.DenominacionPuc,
                        Denominacion = g.Key.Denominacion,

                        Presupuestado = g.Sum(x => x.Presupuestado),
                        Modificado = g.Sum(x => x.Modificado),
                        Vigente = g.Sum(x => x.Vigente),
                        Comprometido = g.Sum(x => x.Comprometido),
                        Bloqueado = g.Sum(x => x.Bloqueado),
                        Causado = g.Sum(x => x.Causado),
                        Pagado = g.Sum(x => x.Pagado),
                        Deuda = g.Sum(x => x.Deuda),
                        Disponibilidad = g.Sum(x => x.Disponibilidad),
                        Asignacion = g.Sum(x => x.Asignacion),
                        DisponibilidadFinan = g.Sum(x => x.DisponibilidadFinan),
                    })
                    .ToList();

            return resultGROUP;


        }


        public async Task<ResultDto<List<PreDenominacionDto?>>> GetPreVDenominacionPuc(FilterPreDenominacionDto filter)
        {
            ResultDto<List<PreDenominacionDto?>> result = new ResultDto<List<PreDenominacionDto?>>(null);

            List<PreDenominacionDto> resultDetail = new List<PreDenominacionDto>();
            List<PreDenominacionDto> resultGroup = new List<PreDenominacionDto>();
            try
            {
                filter.Nivel = 4;
                var preVSaldoAA = await GetPreVDenominacionPucAA(filter);

                var preVSaldoBB = await GetPreVDenominacionPucBB(filter);

                var preVSaldoCC = await GetPreVDenominacionPucCC(filter);

                resultDetail = GetResultDetail(preVSaldoAA, preVSaldoBB, preVSaldoCC);
                List<PreDenominacionDto> resultGROUPNivel4 = GetPreDenominacionDtoAgrupado(resultDetail);
                if (resultGROUPNivel4.Count > 0)
                {
                    resultGroup.AddRange(resultGROUPNivel4);
                       
                }

                filter.Nivel = 3;
                preVSaldoAA = await GetPreVDenominacionPucAA(filter);

                preVSaldoBB = await GetPreVDenominacionPucBB(filter);

                preVSaldoCC = await GetPreVDenominacionPucCC(filter);

                resultDetail = GetResultDetail(preVSaldoAA, preVSaldoBB, preVSaldoCC);
                List<PreDenominacionDto> resultGROUPNivel3 = GetPreDenominacionDtoAgrupado(resultDetail);
                if (resultGROUPNivel3.Count > 0)
                {
                    resultGroup.AddRange(resultGROUPNivel3);

                }


                filter.Nivel = 2;
                preVSaldoAA = await GetPreVDenominacionPucAA(filter);

                preVSaldoBB = await GetPreVDenominacionPucBB(filter);

                preVSaldoCC = await GetPreVDenominacionPucCC(filter);

                resultDetail = GetResultDetail(preVSaldoAA, preVSaldoBB, preVSaldoCC);
                List<PreDenominacionDto> resultGROUPNivel2 = GetPreDenominacionDtoAgrupado(resultDetail);
                if (resultGROUPNivel2.Count > 0)
                {
                    resultGroup.AddRange(resultGROUPNivel2);

                }

                filter.Nivel = 1;
                preVSaldoAA = await GetPreVDenominacionPucAA(filter);

                preVSaldoBB = await GetPreVDenominacionPucBB(filter);

                preVSaldoCC = await GetPreVDenominacionPucCC(filter);

                resultDetail = GetResultDetail(preVSaldoAA, preVSaldoBB, preVSaldoCC);
                List<PreDenominacionDto> resultGROUPNivel1 = GetPreDenominacionDtoAgrupado(resultDetail);
                if (resultGROUPNivel1.Count > 0)
                {
                    resultGroup.AddRange(resultGROUPNivel1);

                }


                if (resultGroup.Count > 0)
                {

                    result.IsValid = true;
                    var sortData= resultGroup.OrderBy(x => x.CodigoPartida).ThenBy(x => x.CodigoGenerica).ThenBy(x => x.CodigoEspecifica).ThenBy(x => x.CodigoSubEspecifica).ThenBy(x => x.CodigoNivel5).ToList();
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

        public async Task<PRE_PLAN_UNICO_CUENTAS> GetPrePlanUnicoCuentas(FilterPreDenominacionDto filter,PRE_V_SALDOS item)
        {
            PRE_PLAN_UNICO_CUENTAS? prePlanUnicoCuentas = new PRE_PLAN_UNICO_CUENTAS();

            switch (filter.Nivel)
            {
                case 4:
                    prePlanUnicoCuentas = await _context.PRE_PLAN_UNICO_CUENTAS.
                        Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto &&
                         x.CODIGO_GRUPO == item.CODIGO_GRUPO &&
                         x.CODIGO_NIVEL1 == item.CODIGO_PARTIDA &&
                         x.CODIGO_NIVEL2 == "00" &&
                         x.CODIGO_NIVEL3 == "00" &&
                         x.CODIGO_NIVEL4 == "00" &&
                         x.CODIGO_NIVEL5 == "00").FirstOrDefaultAsync();
                    break;
                case 3:
                    prePlanUnicoCuentas = await _context.PRE_PLAN_UNICO_CUENTAS.
                       Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto &&
                        x.CODIGO_GRUPO == item.CODIGO_GRUPO &&
                        x.CODIGO_NIVEL1 == item.CODIGO_PARTIDA &&
                        x.CODIGO_NIVEL2 == item.CODIGO_GENERICA &&
                        x.CODIGO_NIVEL3 == "00" &&
                        x.CODIGO_NIVEL4 == "00" &&
                        x.CODIGO_NIVEL5 == "00").FirstOrDefaultAsync();
                    break;
                case 2:
                    prePlanUnicoCuentas = await _context.PRE_PLAN_UNICO_CUENTAS.
                     Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto &&
                      x.CODIGO_GRUPO == item.CODIGO_GRUPO &&
                      x.CODIGO_NIVEL1 == item.CODIGO_PARTIDA &&
                      x.CODIGO_NIVEL2 == item.CODIGO_GENERICA &&
                      x.CODIGO_NIVEL3 == item.CODIGO_ESPECIFICA &&
                      x.CODIGO_NIVEL4 == "00" &&
                      x.CODIGO_NIVEL5 == "00").FirstOrDefaultAsync();
                    break;
                case 1:
                    prePlanUnicoCuentas = await _context.PRE_PLAN_UNICO_CUENTAS.
                    Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto &&
                     x.CODIGO_GRUPO == item.CODIGO_GRUPO &&
                     x.CODIGO_NIVEL1 == item.CODIGO_PARTIDA &&
                     x.CODIGO_NIVEL2 == item.CODIGO_GENERICA &&
                     x.CODIGO_NIVEL3 == item.CODIGO_ESPECIFICA &&
                     x.CODIGO_NIVEL4 == item.CODIGO_SUBESPECIFICA &&
                     x.CODIGO_NIVEL5 == "00").FirstOrDefaultAsync();
                    break;
                default:
                    // code block
                    break;
            }

            return prePlanUnicoCuentas;
        }

        public async Task<List<AAPreDenominacionDto>> GetPreVDenominacionPucAA(FilterPreDenominacionDto filter)
        {
            List<AAPreDenominacionDto> result = new List<AAPreDenominacionDto>();

            try
            {
                var filterPreVSaldo = await _context.PRE_V_SALDOS.Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto && x.FINANCIADO_ID == filter.FinanciadoId).ToListAsync();
                if (filterPreVSaldo.Count > 0)
                {
                    foreach (var item in filterPreVSaldo)
                    {
                        PRE_PLAN_UNICO_CUENTAS? prePlanUnicoCuentas = new PRE_PLAN_UNICO_CUENTAS();
                        prePlanUnicoCuentas = await GetPrePlanUnicoCuentas(filter, item);
                     


                        if (prePlanUnicoCuentas != null)
                        {
                            AAPreDenominacionDto resultItem = new AAPreDenominacionDto();
                            resultItem.CodigoPresupuesto = (int)item.CODIGO_PRESUPUESTO;
                            resultItem.CodigoSaldo = item.CODIGO_SALDO;
                            resultItem.CodigoSector = item.CODIGO_SECTOR;
                            resultItem.CodigoPrograma = item.CODIGO_PROGRAMA;
                            resultItem.CodigoSubPrograma = item.CODIGO_SUBPROGRAMA;
                            resultItem.CodigoProyecto = item.CODIGO_PROYECTO;
                            resultItem.CodigoActividad = item.CODIGO_ACTIVIDAD;
                            resultItem.DenominacionIcp = item.DENOMINACION_ICP;
                            resultItem.CodigoPartida = item.CODIGO_GRUPO + item.CODIGO_PARTIDA;
                            resultItem.CodigoGenerica = item.CODIGO_GENERICA;
                            resultItem.CodigoEspecifica = item.CODIGO_ESPECIFICA;
                            resultItem.CodigoNivel5 = item.CODIGO_NIVEL5;
                            resultItem.DenominacionPuc = prePlanUnicoCuentas.DENOMINACION;
                            resultItem.Denominacion = prePlanUnicoCuentas.DENOMINACION;
                            resultItem.FinanciadoId = (int)item.FINANCIADO_ID;
                            resultItem.Presupuestado = 0;
                            if (resultItem.Asignacion > 0)
                            {
                                resultItem.Presupuestado = item.PRESUPUESTADO;
                            }
                            resultItem.Asignacion = item.ASIGNACION;
                            resultItem.Modificado = 0;
                            if (filter.FinanciadoId != 92)
                            {
                                resultItem.Modificado = item.MODIFICADO;
                            }

                            resultItem.Comprometido = item.COMPROMETIDO;
                            resultItem.Bloqueado = item.BLOQUEADO;
                            result.Add(resultItem);
                        }

                    }



                }


                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }


          
        }

        public async Task<List<BBPreDenominacionDto>> GetPreVDenominacionPucBB(FilterPreDenominacionDto filter)
        {
            List<BBPreDenominacionDto> result = new List<BBPreDenominacionDto>();

            try
            {

                var filterPreVSaldo = await _context.PRE_V_SALDOS.Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto && x.FINANCIADO_ID == filter.FinanciadoId).ToListAsync();
                if (filterPreVSaldo.Count > 0)
                {
                    foreach (var item in filterPreVSaldo)
                    {
                        PRE_PLAN_UNICO_CUENTAS? prePlanUnicoCuentas = new PRE_PLAN_UNICO_CUENTAS();
                        prePlanUnicoCuentas = await GetPrePlanUnicoCuentas(filter, item);

                        var preVSaldosDiarios = await _context.PRE_SALDOS_DIARIOS
                            .Where(x => x.CODIGO_SALDO == item.CODIGO_SALDO &&
                                        x.FECHA_SALDO >= filter.FechaDesde &&
                                        x.FECHA_SALDO <= filter.FechaHasta).FirstOrDefaultAsync();


                        if (prePlanUnicoCuentas != null && preVSaldosDiarios != null)
                        {
                            BBPreDenominacionDto resultItem = new BBPreDenominacionDto();
                            resultItem.CodigoPresupuesto = (int)item.CODIGO_PRESUPUESTO;
                            resultItem.CodigoSaldo = item.CODIGO_SALDO;
                            resultItem.CodigoSector = item.CODIGO_SECTOR;
                            resultItem.CodigoPrograma = item.CODIGO_PROGRAMA;
                            resultItem.CodigoSubPrograma = item.CODIGO_SUBPROGRAMA;
                            resultItem.CodigoProyecto = item.CODIGO_PROYECTO;
                            resultItem.CodigoActividad = item.CODIGO_ACTIVIDAD;
                            resultItem.DenominacionIcp = item.DENOMINACION_ICP;
                            resultItem.CodigoPartida = item.CODIGO_GRUPO + item.CODIGO_PARTIDA;
                            resultItem.CodigoGenerica = item.CODIGO_GENERICA;
                            resultItem.CodigoEspecifica = item.CODIGO_ESPECIFICA;
                            resultItem.CodigoSubEspecifica = item.CODIGO_SUBESPECIFICA;
                            resultItem.CodigoNivel5 = item.CODIGO_NIVEL5;
                            resultItem.DenominacionPuc = prePlanUnicoCuentas.DENOMINACION;

                            resultItem.FinanciadoId = (int)item.FINANCIADO_ID;
                            resultItem.Vigente = 0;
                            resultItem.Asignacion = item.ASIGNACION;
                            resultItem.Modificado = 0;
                            resultItem.Comprometido = 0;
                            resultItem.Causado = 0;
                            resultItem.Pagado = 0;
                            if (filter.FinanciadoId != 92)
                            {
                                resultItem.Modificado = preVSaldosDiarios.MODIFICADO;
                                resultItem.Comprometido = preVSaldosDiarios.COMPROMETIDO;
                                resultItem.Causado = preVSaldosDiarios.CAUSADO;
                                resultItem.Pagado = preVSaldosDiarios.PAGADO;
                            }

                            resultItem.Bloqueado = preVSaldosDiarios.BLOQUEADO;
                            resultItem.Disponibilidad = 0;


                            result.Add(resultItem);
                        }

                    }



                }


                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }


        }

        public async Task<List<CCPreDenominacionDto>> GetPreVDenominacionPucCC(FilterPreDenominacionDto filter)
        {
            List<CCPreDenominacionDto> result = new List<CCPreDenominacionDto>();

            try
            {

                var filterPreVSaldo = await _context.PRE_V_SALDOS.Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto && x.FINANCIADO_ID == filter.FinanciadoId).ToListAsync();
                if (filterPreVSaldo.Count > 0)
                {
                    foreach (var item in filterPreVSaldo)
                    {
                        PRE_PLAN_UNICO_CUENTAS? prePlanUnicoCuentas = new PRE_PLAN_UNICO_CUENTAS();

                        prePlanUnicoCuentas = await GetPrePlanUnicoCuentas(filter, item);

                        var preVSaldosDiarios = await _context.PRE_SALDOS_DIARIOS
                            .Where(x => x.CODIGO_SALDO == item.CODIGO_SALDO &&

                                        x.FECHA_SALDO <= filter.FechaHasta).FirstOrDefaultAsync();


                        if (prePlanUnicoCuentas != null && preVSaldosDiarios != null)
                        {
                            CCPreDenominacionDto resultItem = new CCPreDenominacionDto();
                            resultItem.CodigoPresupuesto = (int)item.CODIGO_PRESUPUESTO;
                            resultItem.CodigoSaldo = item.CODIGO_SALDO;
                            resultItem.CodigoSector = item.CODIGO_SECTOR;
                            resultItem.CodigoPrograma = item.CODIGO_PROGRAMA;
                            resultItem.CodigoSubPrograma = item.CODIGO_SUBPROGRAMA;
                            resultItem.CodigoProyecto = item.CODIGO_PROYECTO;
                            resultItem.CodigoActividad = item.CODIGO_ACTIVIDAD;
                            resultItem.DenominacionIcp = item.DENOMINACION_ICP;
                            resultItem.CodigoPartida = item.CODIGO_GRUPO + item.CODIGO_PARTIDA;
                            resultItem.CodigoGenerica = item.CODIGO_GENERICA;
                            resultItem.CodigoEspecifica = item.CODIGO_ESPECIFICA;
                            resultItem.CodigoSubEspecifica = item.CODIGO_SUBESPECIFICA;
                            resultItem.CodigoNivel5 = item.CODIGO_NIVEL5;
                            resultItem.DenominacionPuc = prePlanUnicoCuentas.DENOMINACION;

                            resultItem.FinanciadoId = (int)item.FINANCIADO_ID;
                            resultItem.Vigente = 0;
                            resultItem.Asignacion = item.ASIGNACION;
                            resultItem.Modificado = 0;
                            resultItem.Comprometido = 0;
                            resultItem.Causado = 0;
                            resultItem.Pagado = 0;
                            if (filter.FinanciadoId != 92)
                            {
                                resultItem.Modificado = preVSaldosDiarios.MODIFICADO;
                                resultItem.Comprometido = preVSaldosDiarios.COMPROMETIDO;
                                resultItem.Causado = preVSaldosDiarios.CAUSADO;
                                resultItem.Pagado = preVSaldosDiarios.PAGADO;
                            }

                            resultItem.Bloqueado = preVSaldosDiarios.BLOQUEADO;
                            resultItem.Disponibilidad = 0;


                            result.Add(resultItem);
                        }

                    }



                }


                return result;
            }
            catch (Exception ex)
            {
                var msg=ex.Message;
                return null;
            }



        }








    }
}

