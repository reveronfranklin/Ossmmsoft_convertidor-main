using System;
using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
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

    }
}

