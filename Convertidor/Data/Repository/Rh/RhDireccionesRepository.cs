using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhDireccionesRepository : IRhDireccionesRepository
    {
		
        private readonly DataContext _context;

        public RhDireccionesRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_DIRECCIONES>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                var result = await _context.RH_DIRECCIONES.DefaultIfEmpty().Where(e => e.CODIGO_PERSONA == codigoPersona).ToListAsync();
        
                return (List<RH_DIRECCIONES>)result; 
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }



    }
}

