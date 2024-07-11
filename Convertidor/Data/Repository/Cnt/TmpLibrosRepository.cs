using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class TmpLibrosRepository : ITmpLibrosRepository
    {
        private readonly DataContextCnt _context;

        public TmpLibrosRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<TMP_LIBROS>> GetAll()
        {
            try
            {
                var result = await _context.TMP_LIBROS.DefaultIfEmpty().ToListAsync();
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
