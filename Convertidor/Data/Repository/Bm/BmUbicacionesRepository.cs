using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Bm
{
    public class BmUbicacionesRepository: IBmUbicacionesRepository
    {
		
        private readonly DataContextBm _context;
     

        public BmUbicacionesRepository(DataContextBm context)
        {
            _context = context;
            
        }
        

        public async Task<List<BM_V_UBICACIONES>> GetAll()
        {
            try
            {
             
                var result = await _context.BM_V_UBICACIONES.DefaultIfEmpty() .ToListAsync();
              
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<BM_V_UBICACIONES> GetByCodigoDirBien(int codigoDirBien)
        {
            try
            {
             
                var result = await _context.BM_V_UBICACIONES.DefaultIfEmpty()
                    .Where(x=>x.CODIGO_DIR_BIEN==codigoDirBien).FirstOrDefaultAsync();
              
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


