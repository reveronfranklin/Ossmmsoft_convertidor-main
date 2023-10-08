using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
    public class RhComunicacionessRepository : IRhComunicacionessRepository
    {
		
        private readonly DataContext _context;

        public RhComunicacionessRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_COMUNICACIONES>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                var result = await _context.RH_COMUNICACIONES.DefaultIfEmpty().Where(e => e.CODIGO_PERSONA == codigoPersona).ToListAsync();
        
                return (List<RH_COMUNICACIONES>)result; 
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

    }
}

