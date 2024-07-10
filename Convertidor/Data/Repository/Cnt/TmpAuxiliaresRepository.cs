using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class TmpAuxiliaresRepository : ITmpAuxiliaresRepository
    {
        private readonly DataContextCnt _context;

        public TmpAuxiliaresRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<TMP_AUXILIARES>> GetAll()
        {
            try
            {
                var result = await _context.TMP_AUXILIARES.DefaultIfEmpty().ToListAsync();
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
