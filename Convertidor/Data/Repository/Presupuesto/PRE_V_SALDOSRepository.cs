using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class PRE_V_SALDOSRepository: IPRE_V_SALDOSRepository
    {
		

        private readonly DataContextPre _context;

        public PRE_V_SALDOSRepository(DataContextPre context)
        {
            _context = context;
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

                return null;@
            }

        }
    }
}

