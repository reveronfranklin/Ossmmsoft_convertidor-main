using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhPersonasRepository: IRhPersonasRepository
    {
		
        private readonly DataContext _context;

        public RhPersonasRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_PERSONAS>> GetAll()
        {
            try
            {

                var result = await _context.RH_PERSONAS.DefaultIfEmpty().ToListAsync();
                return (List<RH_PERSONAS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

    }
}

