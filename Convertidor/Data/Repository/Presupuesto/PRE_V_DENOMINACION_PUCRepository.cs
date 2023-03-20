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
	public class PRE_V_DENOMINACION_PUCRepository : IPRE_V_DENOMINACION_PUCRepository
    {
		

        private readonly DataContextPre _context;

        public PRE_V_DENOMINACION_PUCRepository(DataContextPre context)
        {
            _context = context;
        }

       

        public async Task<IEnumerable<PRE_V_DENOMINACION_PUC>> GetAll(FilterPRE_V_DENOMINACION_PUC filter)
        {
            try
            {

                var result = await _context.PRE_V_DENOMINACION_PUC.DefaultIfEmpty()
                    .Where(x=> x.ANO_SALDO>=filter.AnoDesde && x.ANO_SALDO <= filter.AnoHasta).ToListAsync();
                return (IEnumerable<PRE_V_DENOMINACION_PUC>)result;
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public async Task<IEnumerable<PRE_V_DENOMINACION_PUC>> GetByCodigoPresupuesto(int codigoPresupuesto)
        {
            try
            {

                var result = await _context.PRE_V_DENOMINACION_PUC.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto).ToListAsync();
                return (IEnumerable<PRE_V_DENOMINACION_PUC>)result;
            }
            catch (Exception ex)
            {

                return null;
            }

        }


    }
}

