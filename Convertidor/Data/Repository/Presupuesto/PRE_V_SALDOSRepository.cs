using System;
using System.Data;
using System.Security.Policy;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Microsoft.Data.SqlClient;
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

        public async Task RecalcularSaldo(int codigo_presupuesto)
        {

            // var codigo_presupuestoP = new SqlParameter("@Codigo_Presupuesto", codigo_presupuesto);


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

        
    }
}

