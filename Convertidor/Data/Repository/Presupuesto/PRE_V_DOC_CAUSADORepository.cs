using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class PRE_V_DOC_CAUSADORepository: IPRE_V_DOC_CAUSADORepository
    {
	

        private readonly DataContextPre _context;

        public PRE_V_DOC_CAUSADORepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<List<PRE_V_DOC_CAUSADO>> GetByCodicoSaldo(FilterDocumentosPreVSaldo filter)
        {
            try
            {

                var result = await _context.PRE_V_DOC_CAUSADO.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto && x.CODIGO_SALDO == filter.CodigoSaldo ).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {

                return null;
            }

        }

    }
}

