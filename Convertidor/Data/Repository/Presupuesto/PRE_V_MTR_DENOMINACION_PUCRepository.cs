using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class PRE_V_MTR_DENOMINACION_PUCRepository: IPRE_V_MTR_DENOMINACION_PUCRepository
    {
	
        private readonly DataContextPre _context;

        public PRE_V_MTR_DENOMINACION_PUCRepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<List<PRE_V_MTR_DENOMINACION_PUC>> GetAll()
        {
            try
            {

                var result = await _context.PRE_V_MTR_DENOMINACION_PUC.DefaultIfEmpty().ToListAsync();
                return (List<PRE_V_MTR_DENOMINACION_PUC>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


    }
}

