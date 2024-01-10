﻿using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class PRE_V_DOC_MODIFICADORepository: IPRE_V_DOC_MODIFICADORepository
    {
	

        private readonly DataContextPre _context;

        public PRE_V_DOC_MODIFICADORepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<List<PRE_V_DOC_MODIFICADO>> GetByCodicoSaldo(FilterDocumentosPreVSaldo filter)
        {
            try
            {

                var result = await _context.PRE_V_DOC_MODIFICADO.DefaultIfEmpty()
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

