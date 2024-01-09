using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhProcesoRepository: IRhProcesoRepository
    {
		
        private readonly DataContext _context;

        public RhProcesoRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<RH_PROCESOS> GetByCodigo(int codigoProcesso)
        {
            try
            {
                var result = await _context.RH_PROCESOS.DefaultIfEmpty().Where(e => e.CODIGO_PROCESO == codigoProcesso).FirstOrDefaultAsync();
        
                return (RH_PROCESOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_PROCESOS>> GetAll()
        {
            try
            {
                var result = await _context.RH_PROCESOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

    }
}

