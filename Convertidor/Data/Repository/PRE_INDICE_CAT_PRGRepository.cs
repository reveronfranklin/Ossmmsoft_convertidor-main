using Convertidor.Data.Entities;
using Convertidor.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository
{
    public class PRE_INDICE_CAT_PRGRepository: IPRE_INDICE_CAT_PRGRepository
    {

        private readonly DataContextPre _context;

        public PRE_INDICE_CAT_PRGRepository(DataContextPre context)
        {
            _context = context;
        }

        
        public async Task<IEnumerable<PRE_INDICE_CAT_PRG>> GetByLastDay(int days)
        {
            try
            {
                var fecha = DateTime.Now.AddDays(days * -1);
                var result = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty().ToListAsync();
                return (IEnumerable<PRE_INDICE_CAT_PRG>)result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }
        public async Task<IEnumerable<PRE_INDICE_CAT_PRG>> GetAll()
        {
            try
            {
              
                var result = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty().ToListAsync();
                return (IEnumerable<PRE_INDICE_CAT_PRG>)result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }

    }
}
