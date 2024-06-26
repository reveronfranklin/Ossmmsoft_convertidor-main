using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntLibrosRepository : ICntLibrosRepository
    {
        private readonly DataContextCnt _context;

        public CntLibrosRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_LIBROS>> GetAll()
        {
            try
            {
                var result = await _context.CNT_LIBROS.DefaultIfEmpty().ToListAsync();
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
