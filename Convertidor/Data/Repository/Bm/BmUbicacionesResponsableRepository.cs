using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Bm
{
    public class BmUbicacionesResponsableRepository: IBmUbicacionesResponsableRepository
    {
		
        private readonly DataContextBmConteo _context;
     

        public BmUbicacionesResponsableRepository(DataContextBmConteo context)
        {
            _context = context;
            
        }
        

        public async Task<List<BM_V_UBICA_RESPONSABLE>> GetAll()
        {
            try
            {
             
                var result = await _context.BM_V_UBICA_RESPONSABLE.DefaultIfEmpty() .ToListAsync();
              
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        
        
        public async Task<List<BM_V_UBICA_RESPONSABLE>> GetByUsuarioResponsable(string usuarioResponsable)
        {
            try
            {
             
                var result = await _context.BM_V_UBICA_RESPONSABLE.DefaultIfEmpty() 
                    .Where(x=>x.LOGIN.ToUpper()==usuarioResponsable.ToUpper())
                    .ToListAsync();
              
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


