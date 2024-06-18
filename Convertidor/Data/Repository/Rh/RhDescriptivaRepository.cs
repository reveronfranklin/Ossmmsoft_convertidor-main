using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhDescriptivaRepository: IRhDescriptivaRepository
    {
		
        private readonly DataContext _context;

        public RhDescriptivaRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<RH_DESCRIPTIVAS> GetByCodigoDescriptiva(int descripcionId)
        {
            try
            {
                var result = await _context.RH_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.DESCRIPCION_ID == descripcionId).FirstOrDefaultAsync();
        
                return (RH_DESCRIPTIVAS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        
        
        public async Task<List<RH_DESCRIPTIVAS>> GetByTituloId(int tituloId)
        {
            try
            {
                var result = await _context.RH_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.TITULO_ID == tituloId).ToListAsync();
        
                return (List<RH_DESCRIPTIVAS>)result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        

        public async Task<List<RH_DESCRIPTIVAS>> GetAll()
        {
            try
            {
                var result = await _context.RH_DESCRIPTIVAS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        
        public async Task<bool> GetByIdAndTitulo(int tituloId,int id)
        {
            try
            {
                var descriptiva = await _context.RH_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.TITULO_ID == tituloId && e.DESCRIPCION_ID==id).FirstOrDefaultAsync();
                var result = false;
                if (descriptiva != null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return false;
            }

        }


    }
}

